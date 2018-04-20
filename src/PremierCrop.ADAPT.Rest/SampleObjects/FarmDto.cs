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

namespace SampleObjects
{
    public class FarmDto
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public Guid GrowerUid { get; set; }
    }
}
