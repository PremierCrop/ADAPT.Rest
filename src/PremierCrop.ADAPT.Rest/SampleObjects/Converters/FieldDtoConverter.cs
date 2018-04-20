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
using AgGateway.ADAPT.ApplicationDataModel.Documents;
using AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using PremierCrop.ADAPT.Rest;

namespace SampleObjects.Converters
{
    public class FieldDtoConverter
    {
        private readonly UniqueIdFactory _uniqueIdFactory;

        public FieldDtoConverter(UniqueIdFactory uniqueIdFactory)
        {
            _uniqueIdFactory = uniqueIdFactory;
        }

        public ModelEnvelope<Field> Convert(FieldDto fieldDto)
        {
            if (fieldDto == null)
                return null;

            var farmUniqueId = _uniqueIdFactory.CreateGuid(fieldDto.FarmUid);
            var farmCompoundId = farmUniqueId.ToCompoundIdentifier();

            var fieldUniqueId = _uniqueIdFactory.CreateGuid(fieldDto.Uid);
            var fieldCompoundId = fieldUniqueId.ToCompoundIdentifier();

            var growerUniqueId = _uniqueIdFactory.CreateGuid(fieldDto.GrowerUid);
            var growerCompoundId = growerUniqueId.ToCompoundIdentifier();

            var selfLink = new ReferenceLink
            {
                Id = fieldCompoundId,
                Rel = Relationships.Self,
                Link = $"/Fields/{fieldUniqueId.Source}/{fieldUniqueId.Id}"
            };

            var growerLink = new ReferenceLink
            {
                Id = growerCompoundId,
                Rel = typeof(Grower).ObjectRel(),
                Link = $"/Growers/{growerUniqueId.Source}/{growerUniqueId.Id}"
            };

            var farmLink = new ReferenceLink
            {
                Id = farmCompoundId,
                Rel = typeof(Farm).ObjectRel(),
                Link = $"/Farms/{farmUniqueId.Source}/{farmUniqueId.Id}"
            };

            var cropZonesLink = new ReferenceLink
            {
                Rel = typeof(CropZone).ListRel(),
                Link = $"/Fields/{fieldUniqueId.Source}/{fieldUniqueId.Id}/CropZones"
            };

            var boundaryLink = new ReferenceLink
            {
                Rel = typeof(FieldBoundary).ListRel(),
                Link = $"/Fields/{fieldUniqueId.Source}/{fieldUniqueId.Id}/FieldBoundaries"
            };
            
            var field = new Field()
            {
                Description = fieldDto.Name,
                FarmId = farmCompoundId.ReferenceId
            };

            field.Id.UniqueIds.Add(fieldUniqueId);

            var fieldEnvelope = new ModelEnvelope<Field>(field);
            fieldEnvelope.Links.Add(selfLink);
            fieldEnvelope.Links.Add(growerLink);
            fieldEnvelope.Links.Add(farmLink);
            fieldEnvelope.Links.Add(cropZonesLink);
            fieldEnvelope.Links.Add(boundaryLink);

            return fieldEnvelope;
        }
    }
}
