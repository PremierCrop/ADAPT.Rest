/*******************************************************************************
  * Copyright (c) 2018 Premier Crop Systems, LLC
  * All rights reserved. This program and the accompanying materials
  * are made available under the terms of the Eclipse Public License v1.0
  * which accompanies this distribution, and is available at
  * http://www.eclipse.org/legal/epl-v20.html
  *
  * Contributors:
  *    Keith Reimer - Initial version.
  *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgGateway.ADAPT.ApplicationDataModel.Common;
using AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using PremierCrop.ADAPT.Rest;

namespace SampleObjects.Converters
{
    public class FieldBoundaryDtoConverter
    {
        private readonly UniqueIdFactory _uniqueIdFactory;

        public FieldBoundaryDtoConverter(UniqueIdFactory uniqueIdFactory)
        {
            _uniqueIdFactory = uniqueIdFactory;
        }

        public ModelEnvelope<FieldBoundary> Convert(FieldBoundaryDto fieldBoundaryDto)
        {
            var fieldBoundary = new FieldBoundary
            {
                TimeScopes = new List<TimeScope>()
                {
                    new TimeScope { TimeStamp1 = new DateTime(fieldBoundaryDto.CropYear, 1, 1), TimeStamp2 = new DateTime(fieldBoundaryDto.CropYear, 12, 31) }
                },
                Description = $"{fieldBoundaryDto.CropYear} Boundary"
            };

            var fieldBoundaryUniqueId = fieldBoundary.Id;
            var boundaryUniqueId = _uniqueIdFactory.CreateInt(fieldBoundaryDto.Id);
            fieldBoundaryUniqueId.UniqueIds.Add(boundaryUniqueId);

            var selfLink = new ReferenceLink
            {
                Id = fieldBoundaryUniqueId,
                Rel = Relationships.Self,
                Link = $"/FieldBoundaries/{boundaryUniqueId.Source}/{boundaryUniqueId.Id}"
            };
            
            var fieldUniqueId = _uniqueIdFactory.CreateGuid(fieldBoundaryDto.FieldUid);
            var fieldCompoundId = fieldUniqueId.ToCompoundIdentifier();
            fieldBoundary.FieldId = fieldCompoundId.ReferenceId;

            var fieldLink = new ReferenceLink
            {
                Id = fieldCompoundId,
                Rel = typeof(Field).ObjectRel(),
                Link = $"/Fields/{fieldUniqueId.Source}/{fieldUniqueId.Id}"
            };
            

            // NOTE:  Skipped SpatialData to not introduce Spatial Dependencies in conversion.
            
            var fieldBoundaryEnvelope = new ModelEnvelope<FieldBoundary>(fieldBoundary);
            fieldBoundaryEnvelope.Links.Add(selfLink);
            fieldBoundaryEnvelope.Links.Add(fieldLink);

            return fieldBoundaryEnvelope;
        }
    }
}
