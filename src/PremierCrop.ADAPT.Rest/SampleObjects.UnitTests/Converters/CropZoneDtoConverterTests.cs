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
using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using PremierCrop.ADAPT.Rest;
using SampleObjects.Converters;
using Xunit;

namespace SampleObjects.UnitTests.Converters
{
    public class CropZoneDtoConverterTests
    {
        [Fact]
        public void WHEN_Convert_GIVEN_Valid_Dto_THEN_Get_Valid_Result_With_All_Links()
        {
            var uniqueIdFactory = SampleObjectsIdFactory.Instance;
            var cropZoneDto = new CropZoneDto { Id = 123, FieldUid = Guid.NewGuid(), CropYear = DateTime.Now.Year, Name = $"{DateTime.Now.Year} Corn" };

            var converter = new CropZoneDtoConverter(uniqueIdFactory);
            var field = converter.Convert(cropZoneDto);

            Assert.Equal(cropZoneDto.Id.ToString(), field.Object.Id.UniqueIds.First().Id);
            Assert.Equal(cropZoneDto.Name, field.Object.Description);

            var selfLink = field.Links.Single(l => l.Rel == Relationships.Self);
            Assert.Equal($"/CropZones/{uniqueIdFactory.UniqueIdSource}/{cropZoneDto.Id}", selfLink.Link);

            var fieldsLink = field.Links.Single(l => l.Rel == typeof(Field).ObjectRel());
            Assert.Equal($"/Fields/{uniqueIdFactory.UniqueIdSource}/{cropZoneDto.FieldUid}", fieldsLink.Link);

        }
    }
}
