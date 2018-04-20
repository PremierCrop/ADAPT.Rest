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
    public class FieldsController
    {
        [HttpGet, Route("Fields/{source}/{id}")]
        public ModelEnvelope<Field> Get(string source, string id)
        {
            SampleObjectsIdFactory.ValidateSource(source);

            var dto = SampleRepository.Instance.Fields.Single(f => f.Uid == new Guid(id));
            var converter = new FieldDtoConverter(SampleObjectsIdFactory.Instance);
            return converter.Convert(dto);
        }

        [HttpGet, Route("Growers/{source}/{id}/Fields")]
        public IReadOnlyCollection<ModelEnvelope<Field>> GetByGrower(string source, string id)
        {
            SampleObjectsIdFactory.ValidateSource(source);

            var converter = new FieldDtoConverter(SampleObjectsIdFactory.Instance);

            var list = new List<ModelEnvelope<Field>>();
            foreach (var dto in SampleRepository.Instance.Fields.Where(f => f.GrowerUid == new Guid(id)))
            {
                var envelope = converter.Convert(dto);
                list.Add(envelope);
            }

            return list;
        }


        [HttpGet, Route("Farms/{source}/{id}/Fields")]
        public IReadOnlyCollection<ModelEnvelope<Field>> GetByFarm(string source, string id)
        {
            SampleObjectsIdFactory.ValidateSource(source);

            var converter = new FieldDtoConverter(SampleObjectsIdFactory.Instance);

            var list = new List<ModelEnvelope<Field>>();
            foreach (var dto in SampleRepository.Instance.Fields.Where(f => f.FarmUid == new Guid(id)))
            {
                var envelope = converter.Convert(dto);
                list.Add(envelope);
            }

            return list;
        }
    }
}
