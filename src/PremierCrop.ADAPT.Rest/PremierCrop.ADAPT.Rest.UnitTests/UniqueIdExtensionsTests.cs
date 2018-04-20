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
    public class UniqueIdExtensionsTests
    {
        [Fact]
        public void WHEN_ToCompoundIdentifier_GIVEN_Valid_UniqueId_THEN_Get_Valid_CompoundIdentifier()
        {
            var uniqueId = new UniqueId
            {
                Id = "abc",
                Source = "xyz-abc.com",
                IdType = IdTypeEnum.String,
                SourceType = IdSourceTypeEnum.URI
            };

            var compoundId = uniqueId.ToCompoundIdentifier();
            Assert.NotNull(compoundId);
            // Should only contain the uniqueId passed.
            Assert.Single(compoundId.UniqueIds);
            Assert.Contains(uniqueId, compoundId.UniqueIds);
            
        }
    }
}
