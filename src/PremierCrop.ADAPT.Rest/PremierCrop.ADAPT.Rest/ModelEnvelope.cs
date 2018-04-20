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

using System.Collections.Generic;

namespace PremierCrop.ADAPT.Rest
{
    /// <summary>
    /// Wrapper class for an object with <see cref="ReferenceLink"/>s to related objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelEnvelope<T>
    {
        public ModelEnvelope()
        {
        }

        public ModelEnvelope(T model)
        {
            Object = model;
        }

        public T Object { get; set; }

        public string Type => Object.GetType().Name;

        public List<ReferenceLink> Links { get; set; } = new List<ReferenceLink>();

    }
}
