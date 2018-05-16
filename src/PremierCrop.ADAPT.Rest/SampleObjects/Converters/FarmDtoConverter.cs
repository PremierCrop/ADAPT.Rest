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
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using PremierCrop.ADAPT.Rest;

namespace SampleObjects.Converters
{
    public class FarmDtoConverter
    {
        private readonly UniqueIdFactory _uniqueIdFactory;

        public FarmDtoConverter(UniqueIdFactory uniqueIdFactory)
        {
            _uniqueIdFactory = uniqueIdFactory;
        }

        public ModelEnvelope<Farm> Convert(FarmDto farmDto)
        {

            var farm = new Farm()
            {
                Description = farmDto.Name
            };

            var farmUniqueId = _uniqueIdFactory.CreateGuid(farmDto.Uid);
            var farmCompoundId = farm.Id;
            farmCompoundId.UniqueIds.Add(farmUniqueId);

            var selfLink = new ReferenceLink
            {
                Id = farmCompoundId,
                Rel = Relationships.Self,
                Link = $"/Farms/{farmUniqueId.Source}/{farmUniqueId.Id}",
                Type = "get"
            };

            var growerUniqueId = _uniqueIdFactory.CreateGuid(farmDto.GrowerUid);
            var growerCompoundId = growerUniqueId.ToCompoundIdentifier();

            farm.GrowerId = growerCompoundId.ReferenceId;
            var growerLink = new ReferenceLink
            {
                Id = growerCompoundId,
                Rel = typeof(Grower).ObjectRel(),
                Link = $"/Growers/{growerUniqueId.Source}/{growerUniqueId.Id}",
                Type = "get"
            };

            var fieldsLink = new ReferenceLink
            {
                Rel = typeof(Field).ListRel(),
                Link = $"/Farms/{farmUniqueId.Source}/{farmUniqueId.Id}/Fields",
                Type = "get"
            };

            var farmEnvelope = new ModelEnvelope<Farm>(farm);
            farmEnvelope.Links.Add(growerLink);
            farmEnvelope.Links.Add(selfLink);
            farmEnvelope.Links.Add(fieldsLink);

            return farmEnvelope;
        }
    }
}
