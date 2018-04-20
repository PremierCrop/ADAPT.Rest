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
using AgGateway.ADAPT.ApplicationDataModel.Common;
using Xunit;

namespace PremierCrop.ADAPT.Rest.UnitTests
{
    public class UniqueIdFactoryTests
    {
        private readonly UniqueIdFactory _factory = new UniqueIdFactory {UniqueIdSource = "abcdef", UniqueIdSourceType = IdSourceTypeEnum.URI };

        [Fact]
        public void WHEN_CreateInt_GIVEN_Valid_Value_THEN_Get_Valid_UniqueId()
        {
            var id = int.MaxValue;
            var uniqueId = _factory.CreateInt(id);
            CheckId(uniqueId, id, IdTypeEnum.LongInt);
        }

        [Fact]
        public void WHEN_CreateLong_GIVEN_Valid_Value_THEN_Get_Valid_UniqueId()
        {
            var id = long.MaxValue;
            var uniqueId = _factory.CreateLong(id);
            CheckId(uniqueId, id, IdTypeEnum.LongInt);
        }

        [Fact]
        public void WHEN_CreateUri_GIVEN_Valid_Value_THEN_Get_Valid_UniqueId()
        {
            var id = "www.adapt-test.org";
            var uniqueId = _factory.CreateUri(id);
            CheckId(uniqueId, id, IdTypeEnum.URI);
        }

        [Fact]
        public void WHEN_CreateString_GIVEN_Valid_Value_THEN_Get_Valid_UniqueId()
        {
            var id = "asdf";
            var uniqueId = _factory.CreateString(id);
            CheckId(uniqueId, id, IdTypeEnum.String);
        }


        [Fact]
        public void WHEN_CreateGuid_GIVEN_Valid_Value_THEN_Get_Valid_UniqueId()
        {
            var id = Guid.NewGuid();
            var uniqueId = _factory.CreateGuid(id);
            CheckId(uniqueId, id, IdTypeEnum.UUID);
        }


        private void CheckId(UniqueId uniqueId, object id, IdTypeEnum idType)
        {
            Assert.Equal(id.ToString(), uniqueId.Id);
            Assert.Equal(idType, uniqueId.IdType);
            Assert.Equal(_factory.UniqueIdSource, uniqueId.Source);
            Assert.Equal(_factory.UniqueIdSourceType, uniqueId.SourceType);
        }

        [Fact]
        public void WHEN_ContainsId_GIVEN_Values_THEN_Get_Expected_Result()
        {
            CheckContainsId(_factory.CreateGuid(Guid.NewGuid()));
            CheckContainsId(_factory.CreateInt(int.MinValue));
            CheckContainsId(_factory.CreateLong(long.MinValue));
            CheckContainsId(_factory.CreateString(Guid.NewGuid().ToString()));
            CheckContainsId(_factory.CreateUri(Guid.NewGuid().ToString()));
        }

        void CheckContainsId(UniqueId uniqueId)
        {
            var compoundId = uniqueId.ToCompoundIdentifier();
            // Add some bogus ones
            compoundId.UniqueIds.Add(_factory.CreateGuid(Guid.NewGuid()));
            compoundId.UniqueIds.Add(_factory.CreateInt(int.MaxValue));
            compoundId.UniqueIds.Add(_factory.CreateLong(long.MaxValue));
            compoundId.UniqueIds.Add(_factory.CreateString(Guid.NewGuid().ToString()));
            compoundId.UniqueIds.Add(_factory.CreateUri(Guid.NewGuid().ToString()));

            Assert.True(_factory.ContainsId(compoundId, uniqueId.Id));
            Assert.True(_factory.ContainsId(compoundId, uniqueId.Id, uniqueId.IdType));
            Assert.False(_factory.ContainsId(compoundId, "badId"));
            var otherType = uniqueId.IdType == IdTypeEnum.LongInt ? IdTypeEnum.UUID : IdTypeEnum.LongInt;
            Assert.False(_factory.ContainsId(compoundId, uniqueId.Id, otherType));

        }

        [Fact]
        public void WHEN_ValidateSource_GIVEN_Valid_Source_THEN_NoOp()
        {
            _factory.ValidateSource(_factory.UniqueIdSource);
        }

        [Fact]
        public void WHEN_ValidateSource_GIVEN_Invalid_Source_THEN_Exception()
        {
            var exception = Record.Exception(() => _factory.ValidateSource("bad"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }
    }
}
