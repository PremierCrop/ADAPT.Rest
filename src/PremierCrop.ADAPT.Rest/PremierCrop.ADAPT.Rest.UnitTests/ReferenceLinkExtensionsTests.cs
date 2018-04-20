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
using Xunit;

namespace PremierCrop.ADAPT.Rest.UnitTests
{
    public class ReferenceLinkExtensionsTests
    {
        [Fact]
        public void WHEN_SelfSingle_GIVEN_Array_With_Link_THEN_Get_Link()
        {
            var self = new ReferenceLink {Rel = "self"};
            var links = new[] {
                self,
                new ReferenceLink {Rel = "other"},
            };

            var result = links.SelfLinkSingle();

            Assert.NotNull(result);
            Assert.Equal(self, result);
        }

        [Fact]
        public void WHEN_SelfSingleOrDefault_GIVEN_Array_With_No_Link_THEN_Get_NULL()
        {
            var links = new[] {
                new ReferenceLink {Rel = "other"},
            };

            var result = links.SelfSingleOrDefault();

            Assert.Null(result);
        }

        [Fact]
        public void WHEN_ObjectRelWhere_GIVEN_Array_With_Multiple_Matching_Links_THEN_Get_All()
        {
            var link1 = new ReferenceLink {Rel = "farm"};
            var link2 = new ReferenceLink {Rel = "farm"};
            var links = new[] {
                new ReferenceLink {Rel = "other"},
                link1,
                new ReferenceLink {Rel = "other2"},
                link2,
                new ReferenceLink {Rel = "field"},
            };

            var result = links.ObjectRelWhere<Farm>();
            Assert.Equal(2, result.Count);
            Assert.Contains(link1, result);
            Assert.Contains(link2, result);
        }

        [Fact]
        public void WHEN_ObjectRelWhere_GIVEN_Array_With_No_Matching_Links_THEN_Get_None()
        {
            var links = new[] {
                new ReferenceLink {Rel = "other"},
                new ReferenceLink {Rel = "other2"},
                new ReferenceLink {Rel = "field"},
            };

            var result = links.ObjectRelWhere<Farm>();
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void WHEN_ObjectRelSingle_GIVEN_Array_With_Matching_Link_THEN_Get_It()
        {
            var link = new ReferenceLink {Rel = "farm"};
            var links = new[] {
                new ReferenceLink {Rel = "other"},
                link,
                new ReferenceLink {Rel = "other2"},
                new ReferenceLink {Rel = "field"},
            };

            var result = links.ObjectRelSingle<Farm>();
            Assert.Equal(link, result);
        }

        [Fact]
        public void WHEN_ObjectRelSingleOrDefault_GIVEN_Array_With_Matching_Link_THEN_Get_It()
        {
            var link = new ReferenceLink { Rel = "farm" };
            var links = new[] {
                new ReferenceLink {Rel = "other"},
                link,
                new ReferenceLink {Rel = "other2"},
                new ReferenceLink {Rel = "field"},
            };

            var result = links.ObjectRelSingleOrDefault<Farm>();
            Assert.Equal(link, result);
        }

        [Fact]
        public void WHEN_ObjectRelSingleOrDefault_GIVEN_Array_With_No_Matching_Link_THEN_Get_Null()
        {
            var links = new[] {
                new ReferenceLink {Rel = "other"},
                new ReferenceLink {Rel = "other2"},
                new ReferenceLink {Rel = "field"},
            };

            var result = links.ObjectRelSingleOrDefault<Farm>();
            Assert.Null(result);
        }

        [Fact]
        public void WHEN_ListRelSingle_GIVEN_Array_With_Matching_Link_THEN_Get_It()
        {
            var link = new ReferenceLink { Rel = "farms" };
            var links = new[] {
                new ReferenceLink {Rel = "other"},
                link,
                new ReferenceLink {Rel = "other2"},
                new ReferenceLink {Rel = "field"},
            };

            var result = links.ListRelSingle<Farm>();
            Assert.Equal(link, result);
        }

        [Fact]
        public void WHEN_ListRelSingleOrDefault_GIVEN_Array_With_Matching_Link_THEN_Get_It()
        {
            var link = new ReferenceLink { Rel = "farms" };
            var links = new[] {
                new ReferenceLink {Rel = "other"},
                link,
                new ReferenceLink {Rel = "other2"},
                new ReferenceLink {Rel = "field"},
            };

            var result = links.ListRelSingleOrDefault<Farm>();
            Assert.Equal(link, result);
        }

        [Fact]
        public void WHEN_ListRelSingleOrDefault_GIVEN_Array_With_No_Matching_Link_THEN_Get_Null()
        {
            var links = new[] {
                new ReferenceLink {Rel = "other"},
                new ReferenceLink {Rel = "other2"},
                new ReferenceLink {Rel = "field"},
            };

            var result = links.ListRelSingleOrDefault<Farm>();
            Assert.Null(result);
        }
    }
}
