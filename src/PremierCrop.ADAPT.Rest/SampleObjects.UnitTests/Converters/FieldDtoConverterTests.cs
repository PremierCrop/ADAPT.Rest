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
using AgGateway.ADAPT.ApplicationDataModel.Documents;
using AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using PremierCrop.ADAPT.Rest;
using SampleObjects.Converters;
using Xunit;

namespace SampleObjects.UnitTests.Converters
{
    public class FieldDtoConverterTests
    {
        [Fact]
        public void WHEN_Convert_GIVEN_Valid_Dto_THEN_Get_Valid_Result_With_All_Links()
        {
            var uniqueIdFactory = SampleObjectsIdFactory.Instance;
            var fieldDto = new FieldDto { Uid = Guid.NewGuid(), FarmUid = Guid.NewGuid(), GrowerUid = Guid.NewGuid(), Name = "Test" };

            var converter = new FieldDtoConverter(uniqueIdFactory);
            var field = converter.Convert(fieldDto);

            Assert.Equal(fieldDto.Uid.ToString(), field.Object.Id.UniqueIds.First().Id);
            Assert.Equal(fieldDto.Name, field.Object.Description);
            
            var selfLink = field.Links.Single(l => l.Rel == Relationships.Self);
            Assert.Equal($"/Fields/{uniqueIdFactory.UniqueIdSource}/{fieldDto.Uid}", selfLink.Link);
            Assert.Equal(selfLink.Id.ReferenceId, field.Object.Id.ReferenceId);

            var growerLink = field.Links.Single(l => l.Rel == typeof(Grower).ObjectRel());
            Assert.Equal($"/Growers/{uniqueIdFactory.UniqueIdSource}/{fieldDto.GrowerUid}", growerLink.Link);

            var farmLink = field.Links.Single(l => l.Rel ==typeof(Farm).ObjectRel());
            Assert.Equal($"/Farms/{uniqueIdFactory.UniqueIdSource}/{fieldDto.FarmUid}", farmLink.Link);

            var cropZoneLink = field.Links.Single(l => l.Rel == typeof(CropZone).ListRel());
            Assert.Equal($"/Fields/{uniqueIdFactory.UniqueIdSource}/{fieldDto.Uid}/CropZones", cropZoneLink.Link);

            var boundaryLink = field.Links.Single(l => l.Rel == typeof(FieldBoundary).ListRel());
            Assert.Equal($"/Fields/{uniqueIdFactory.UniqueIdSource}/{fieldDto.Uid}/FieldBoundaries", boundaryLink.Link);

        }
    }
}
