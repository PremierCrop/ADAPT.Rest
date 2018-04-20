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
using Microsoft.AspNetCore.Mvc;
using PremierCrop.ADAPT.Rest;
using SampleObjects;
using SampleObjects.Converters;

namespace Sample.WebApi.Logistics
{
    public class FarmsController
    {
        [HttpGet, Route("Farms/{source}/{id}")]
        public ModelEnvelope<Farm> Get(string source, string id)
        {
            SampleObjectsIdFactory.ValidateSource(source);

            var dto = SampleRepository.Instance.Farms.Single(f => f.Uid == new Guid(id));
            var converter = new FarmDtoConverter(SampleObjectsIdFactory.Instance);
            return converter.Convert(dto);
        }

        [HttpGet, Route("Growers/{source}/{id}/Farms")]
        public IReadOnlyCollection<ModelEnvelope<Farm>> GetByGrower(string source, string id)
        {
            SampleObjectsIdFactory.ValidateSource(source);

            var converter = new FarmDtoConverter(SampleObjectsIdFactory.Instance);

            var list = new List<ModelEnvelope<Farm>>();
            foreach (var dto in SampleRepository.Instance.Farms.Where(f => f.GrowerUid == new Guid(id)))
            {
                var envelope = converter.Convert(dto);
                list.Add(envelope);
            }

            return list;
        }
    }
}
