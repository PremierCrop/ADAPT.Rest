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
    public class GrowerDtoConverter
    {
        private readonly UniqueIdFactory _uniqueIdFactory;

        public GrowerDtoConverter(UniqueIdFactory uniqueIdFactory)
        {
            _uniqueIdFactory = uniqueIdFactory;
        }

        public ModelEnvelope<Grower> Convert(GrowerDto pmGrower)
        {
            var grower = new Grower()
            {
                Name = pmGrower.Name
            };

            var growerCompoundId = grower.Id;
            var growerUniqueId = _uniqueIdFactory.CreateGuid(pmGrower.Uid);
            growerCompoundId.UniqueIds.Add(growerUniqueId);

            var growerLink = new ReferenceLink
            {
                Id = growerCompoundId,
                Rel = Relationships.Self,
                Link = $"/Growers/{growerUniqueId.Source}/{growerUniqueId.Id}",
                Type = "get"
            };
            var farmsLink = new ReferenceLink
            {
                Rel = typeof(Farm).ListRel(),
                Link = $"/Growers/{growerUniqueId.Source}/{growerUniqueId.Id}/Farms"
            };
            var fieldsLink = new ReferenceLink
            {
                Rel = typeof(Field).ListRel(),
                Link = $"/Growers/{growerUniqueId.Source}/{growerUniqueId.Id}/Fields"
            };

            grower.Id.UniqueIds.Add(growerUniqueId);

            var growerEnvelope = new ModelEnvelope<Grower>(grower);
            growerEnvelope.Links.Add(growerLink);
            growerEnvelope.Links.Add(farmsLink);
            growerEnvelope.Links.Add(fieldsLink);

            return growerEnvelope;
        }
    }
}
