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
using AgGateway.ADAPT.ApplicationDataModel.Common;

namespace PremierCrop.ADAPT.Rest
{
    /// <summary>
    /// Extension methods for working with <see cref="UniqueId"/> instances.
    /// </summary>
    public static class UniqueIdExtensions
    {
        /// <summary>
        /// Generates a <see cref="CompoundIdentifier"/> and adds the <see cref="UniqueId"/> to its UniqueIds list.
        /// </summary>
        /// <param name="uniqueId">The <see cref="UniqueId"/> to generate a <see cref="CompoundIdentifier"/> for.</param>
        /// <returns>A new <see cref="CompoundIdentifier"/>.</returns>
        public static CompoundIdentifier ToCompoundIdentifier(this UniqueId uniqueId)
        {
            var id = CompoundIdentifierFactory.Instance.Create();
            id.UniqueIds.Add(uniqueId);

            return id;
        }
    }
}
