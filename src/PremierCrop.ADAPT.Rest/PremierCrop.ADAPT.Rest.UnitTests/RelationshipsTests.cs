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
using AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using Xunit;

namespace PremierCrop.ADAPT.Rest.UnitTests
{
    public class RelationshipsTests
    {
        [Fact]
        public void WHEN_ObjectRel_GIVEN_Type_THEN_Get_Expected_Rel()
        {
            Assert.Equal("farm", typeof(Farm).ObjectRel());
            Assert.Equal("grower", typeof(Grower).ObjectRel());
            Assert.Equal("fieldboundary", typeof(FieldBoundary).ObjectRel());
        }

        [Fact]
        public void WHEN_ListRel_GIVEN_Type_THEN_Get_Expected_Rel()
        {
            Assert.Equal("farms", typeof(Farm).ListRel());
            Assert.Equal("growers", typeof(Grower).ListRel());
            Assert.Equal("fieldboundaries", typeof(FieldBoundary).ListRel());
        }
    }
}
