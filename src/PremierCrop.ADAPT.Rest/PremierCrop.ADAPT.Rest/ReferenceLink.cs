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

using AgGateway.ADAPT.ApplicationDataModel.Common;

namespace PremierCrop.ADAPT.Rest
{
    /// <summary>
    /// Represents an HATEOAS link.  A nullable ADAPT CompoundIdentifier may be specified when the link represents a single ADAPT object.
    /// </summary>
    public class ReferenceLink
    {
        /// <summary>
        /// The ADAPT <see cref="CompoundIdentifier"/>, when applicable.
        /// </summary>
        public CompoundIdentifier Id { get; set; }

        /// <summary>
        /// The relationship of the linked resource to the current resource.
        /// </summary>
        public string Rel { get; set; }
        /// <summary>
        /// The uri link to the resource.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// The http REST verb supported.
        /// </summary>
        public string Type { get; set; } = "get";
    }
}
