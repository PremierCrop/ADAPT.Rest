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
    public class FieldBoundaryDtoConverterTests
    {
        [Fact]
        public void WHEN_Convert_GIVEN_Valid_Dto_THEN_Get_Valid_Result_With_All_Links()
        {
            var uniqueIdFactory = SampleObjectsIdFactory.Instance;
            var fieldBoundaryDto = new FieldBoundaryDto { Id = 123, FieldUid = Guid.NewGuid(), CropYear = DateTime.Now.Year};

            var converter = new FieldBoundaryDtoConverter(uniqueIdFactory);
            var envelope = converter.Convert(fieldBoundaryDto);

            Assert.Equal(fieldBoundaryDto.Id.ToString(), envelope.Object.Id.UniqueIds.First().Id);
            var timeScope = envelope.Object.TimeScopes.Single();
            Assert.Equal(new DateTime(DateTime.Now.Year, 1, 1), timeScope.TimeStamp1);
            Assert.Equal(new DateTime(DateTime.Now.Year, 12, 31), timeScope.TimeStamp2);

            var fieldsLink = envelope.Links.Single(l => l.Rel == typeof(Field).ObjectRel());
            Assert.Equal($"/Fields/{uniqueIdFactory.UniqueIdSource}/{fieldBoundaryDto.FieldUid}", fieldsLink.Link);

        }
    }
}
