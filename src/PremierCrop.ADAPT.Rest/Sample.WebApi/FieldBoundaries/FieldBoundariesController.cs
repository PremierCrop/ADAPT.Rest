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
using AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using Microsoft.AspNetCore.Mvc;
using PremierCrop.ADAPT.Rest;
using SampleObjects;
using SampleObjects.Converters;

namespace Sample.WebApi.FieldBoundaries
{
    public class FieldBoundariesController
    {
        [HttpGet, Route("FieldBoundaries/{source}/{id}")]
        public ModelEnvelope<FieldBoundary> Get(string source, string id)
        {
            SampleObjectsIdFactory.ValidateSource(source);

            var dto = SampleRepository.Instance.FieldBoundaries.Single(z => z.Id == int.Parse(id));
            var converter = new FieldBoundaryDtoConverter(SampleObjectsIdFactory.Instance);
            return converter.Convert(dto);
        }

        [HttpGet, Route("Fields/{source}/{id}/FieldBoundaries/{cropYear:int?}")]
        public IReadOnlyCollection<ModelEnvelope<FieldBoundary>> GetByField(string source, string id, int? cropYear)
        {
            SampleObjectsIdFactory.ValidateSource(source);

            var converter = new FieldBoundaryDtoConverter(SampleObjectsIdFactory.Instance);

            var list = new List<ModelEnvelope<FieldBoundary>>();

            var boundaries = SampleRepository.Instance.FieldBoundaries.Where(z => z.FieldUid == new Guid(id));
            if (cropYear.HasValue)
                boundaries = boundaries.Where(b => b.CropYear == cropYear);

            foreach (var dto in boundaries)
            {
                var envelope = converter.Convert(dto);
                list.Add(envelope);
            }

            return list;
        }
    }
}
