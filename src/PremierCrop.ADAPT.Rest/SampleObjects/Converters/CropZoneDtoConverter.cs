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
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using PremierCrop.ADAPT.Rest;

namespace SampleObjects.Converters
{
    public class CropZoneDtoConverter
    {
        private readonly UniqueIdFactory _uniqueIdFactory;

        public CropZoneDtoConverter(UniqueIdFactory uniqueIdFactory)
        {
            _uniqueIdFactory = uniqueIdFactory;
        }

        public ModelEnvelope<CropZone> Convert(CropZoneDto cropZoneDto)
        {
            var fieldUniqueId = _uniqueIdFactory.CreateGuid(cropZoneDto.FieldUid);
            var fieldCompoundId = fieldUniqueId.ToCompoundIdentifier();

            var cropZoneUniqueId = _uniqueIdFactory.CreateInt(cropZoneDto.Id);
            var cropZoneCompoundId = cropZoneUniqueId.ToCompoundIdentifier();

            var selfLink = new ReferenceLink
            {
                Id = cropZoneCompoundId,
                Rel = Relationships.Self,
                Link = $"/CropZones/{cropZoneUniqueId.Source}/{cropZoneUniqueId.Id}"
            };

            var fieldLink = new ReferenceLink
            {
                Id = fieldCompoundId,
                Rel = typeof(Field).Name.ToLower(),
                Link = $"/Fields/{fieldUniqueId.Source}/{fieldUniqueId.Id}"
            };
            var cropZone = new CropZone()
            {
                Description = cropZoneDto.Name,
                FieldId = fieldCompoundId.ReferenceId,
                TimeScopes = new List<TimeScope>()
                {
                    new TimeScope { TimeStamp1 = new DateTime(cropZoneDto.CropYear, 1, 1), TimeStamp2 = new DateTime(cropZoneDto.CropYear, 12, 31) }
                }
            };

            // NOTE:  Skipped BoundingRegion to not introduce Spatial Dependencies in conversion.
            
            cropZone.Id.UniqueIds.Add(cropZoneUniqueId);
            var cropZoneEnvelope = new ModelEnvelope<CropZone>(cropZone);
            cropZoneEnvelope.Links.Add(selfLink);
            cropZoneEnvelope.Links.Add(fieldLink);

            return cropZoneEnvelope;
        }
    }
}
