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
using PremierCrop.ADAPT.Rest;

namespace SampleObjects
{
    public class SampleObjectsIdFactory
    {
        private SampleObjectsIdFactory()
        {   
        }

        public static readonly UniqueIdFactory Instance = new UniqueIdFactory
        {
            UniqueIdSource = "sample.test.xyz",
            UniqueIdSourceType = IdSourceTypeEnum.URI
        };

        public static void ValidateSource(string source)
        {
            if (!Instance.UniqueIdSource.Equals(source, StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException($"Unknown UniqueId source '{source}.'");
        }
    }
}
