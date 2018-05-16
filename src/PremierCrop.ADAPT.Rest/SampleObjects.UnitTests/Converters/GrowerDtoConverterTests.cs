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
    public class GrowerDtoConverterTests
    {
        [Fact]
        public void WHEN_Convert_GIVEN_Valid_Dto_THEN_Get_Valid_Result_With_All_Links()
        {
            var uniqueIdFactory = SampleObjectsIdFactory.Instance;
            var growerDto = new GrowerDto { Uid = Guid.NewGuid(), Name = "Test" };

            var converter = new GrowerDtoConverter(uniqueIdFactory);
            var grower = converter.Convert(growerDto);

            Assert.Equal(growerDto.Uid.ToString(), grower.Object.Id.UniqueIds.First().Id);
            Assert.Equal(growerDto.Name, grower.Object.Name);

            var selfLink = grower.Links.Single(l => l.Rel == Relationships.Self);
            Assert.Equal($"/Growers/{uniqueIdFactory.UniqueIdSource}/{growerDto.Uid}", selfLink.Link);
            Assert.Equal(selfLink.Id.ReferenceId, grower.Object.Id.ReferenceId);

            var farmsLink = grower.Links.Single(l => l.Rel == typeof(Farm).ListRel());
            Assert.Equal($"/Growers/{uniqueIdFactory.UniqueIdSource}/{growerDto.Uid}/Farms", farmsLink.Link);

            var fieldsLink = grower.Links.Single(l => l.Rel == typeof(Field).ListRel());
            Assert.Equal($"/Growers/{uniqueIdFactory.UniqueIdSource}/{growerDto.Uid}/Fields", fieldsLink.Link);

        }
    }
}
