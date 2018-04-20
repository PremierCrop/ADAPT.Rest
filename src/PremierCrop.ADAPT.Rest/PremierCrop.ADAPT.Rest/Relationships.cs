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

namespace PremierCrop.ADAPT.Rest
{
    /// <summary>
    /// Helper class for defining the "rel" values for a <see cref="ReferenceLink"/>, based on the Type. 
    /// </summary>
    public static class Relationships
    {
        /// <summary>
        /// The Self rel value.
        /// </summary>
        public const string Self = "self";

        /// <summary>
        /// The "rel" value for a single object, based on the type.  Will convert the Type name to lowercase.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to generate the rel for.</param>
        /// <returns>The rel value for the type.</returns>
        /// <example>
        /// typeof(Farm).ObjRel();  // returns "farms"
        /// typeof(FieldBoundary).ObjRel();  // returns "fieldboundary"
        /// </example>
        public static string ObjectRel(this Type type)
        {
            return type.Name.ToLower();
        }

        /// <summary>
        /// The "rel" value for a set of objects of a given type.  Will convert the Type name to lowercase and pluralize it.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to generate the rel for.</param>
        /// <returns>The rel value for a set of objects of the type.</returns>
        /// <example>
        /// typeof(Farm).ListRel();  // returns "farms"
        /// typeof(FieldBoundary).ListRel();  // returns "fieldboundaries"
        /// </example>
        public static string ListRel(this Type type)
        {
            var name = type.Name.ToLower();
            return !name.EndsWith("y") ? $"{type.Name.ToLower()}s"
                : $"{type.Name.Substring(0, type.Name.Length - 1).ToLower()}ies";
        }

    }
}
