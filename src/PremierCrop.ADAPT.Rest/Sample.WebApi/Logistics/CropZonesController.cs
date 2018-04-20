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
    public class CropZonesController
    {
        [HttpGet, Route("CropZones/{source}/{id}")]
        public ModelEnvelope<CropZone> Get(string source, string id)
        {
            SampleObjectsIdFactory.ValidateSource(source);

            var dto = SampleRepository.Instance.CropZones.Single(z => z.Id == int.Parse(id));
            var converter = new CropZoneDtoConverter(SampleObjectsIdFactory.Instance);
            return converter.Convert(dto);
        }

        [HttpGet, Route("Fields/{source}/{id}/CropZones/{cropYear:int?}")]
        public IReadOnlyCollection<ModelEnvelope<CropZone>> GetByField(string source, string id, int? cropYear)
        {
            SampleObjectsIdFactory.ValidateSource(source);

            var converter = new CropZoneDtoConverter(SampleObjectsIdFactory.Instance);

            var list = new List<ModelEnvelope<CropZone>>();
            var zones = SampleRepository.Instance.CropZones.Where(z => z.FieldUid == new Guid(id));
            if (cropYear.HasValue)
                zones = zones.Where(z => z.CropYear == cropYear);
            
            foreach (var dto in zones)
            {
                var envelope = converter.Convert(dto);
                list.Add(envelope);
            }

            return list;
        }
    }
}
