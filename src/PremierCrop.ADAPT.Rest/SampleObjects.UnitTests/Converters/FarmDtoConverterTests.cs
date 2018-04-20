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
    public class FarmDtoConverterTests
    {
        [Fact]
        public void WHEN_Convert_GIVEN_Valid_Dto_THEN_Get_Valid_Result_With_All_Links()
        {
            var uniqueIdFactory = SampleObjectsIdFactory.Instance;
            var farmDto = new FarmDto { Uid = Guid.NewGuid(), GrowerUid = Guid.NewGuid(), Name = "Test" };

            var converter = new FarmDtoConverter(uniqueIdFactory);
            var field = converter.Convert(farmDto);

            Assert.Equal(farmDto.Uid.ToString(), field.Object.Id.UniqueIds.First().Id);
            Assert.Equal(farmDto.Name, field.Object.Description);

            var selfLink = field.Links.Single(l => l.Rel == Relationships.Self);
            Assert.Equal($"/Farms/{uniqueIdFactory.UniqueIdSource}/{farmDto.Uid}", selfLink.Link);

            var growerLink = field.Links.Single(l => l.Rel == typeof(Grower).ObjectRel());
            Assert.Equal($"/Growers/{uniqueIdFactory.UniqueIdSource}/{farmDto.GrowerUid}", growerLink.Link);
            
            var fieldsLink = field.Links.Single(l => l.Rel == typeof(Field).ListRel());
            Assert.Equal($"/Farms/{uniqueIdFactory.UniqueIdSource}/{farmDto.Uid}/Fields", fieldsLink.Link);

        }
    }
}
