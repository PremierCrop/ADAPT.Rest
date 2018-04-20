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
using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.Common;

namespace PremierCrop.ADAPT.Rest
{
    /// <summary>
    /// A factory for creating <see cref="UniqueId"/> instances for a given UniqueIdSource and <see cref="IdSourceTypeEnum"/>.  
    /// </summary>
    public class UniqueIdFactory
    {
        public string UniqueIdSource { get; set; }
        public IdSourceTypeEnum UniqueIdSourceType { get; set; } = IdSourceTypeEnum.URI;

        /// <summary>
        /// Creates an <see cref="UniqueId"/> of <see cref="IdTypeEnum"/>.UUID with the specified Guid value as the Id,
        /// and the factory's UniqueIdSource and UniqueIdSourceType values.
        /// </summary>
        /// <param name="uid">The id value.</param>
        /// <returns>A new <see cref="UniqueId"/>.</returns>
        public UniqueId CreateGuid(Guid uid)
        {
            return new UniqueId
            {
                Source = UniqueIdSource,
                IdType = IdTypeEnum.UUID,
                Id = uid.ToString(),
                SourceType = UniqueIdSourceType
            };
        }

        /// <summary>
        /// Creates an <see cref="UniqueId"/> of <see cref="IdTypeEnum"/>.LongInt with the specified integer value as the Id,
        /// and the factory's UniqueIdSource and UniqueIdSourceType values.
        /// </summary>
        /// <param name="id">The id value.</param>
        /// <returns>A new <see cref="UniqueId"/>.</returns>
        public UniqueId CreateInt(int id)
        {
            return new UniqueId
            {
                Source = UniqueIdSource,
                IdType = IdTypeEnum.LongInt,
                Id = id.ToString(),
                SourceType = UniqueIdSourceType
            };
        }

        /// <summary>
        /// Creates an <see cref="UniqueId"/> of <see cref="IdTypeEnum"/>.LongInt with the specified long value as the Id,
        /// and the factory's UniqueIdSource and UniqueIdSourceType values.
        /// </summary>
        /// <param name="id">The id value.</param>
        /// <returns>A new <see cref="UniqueId"/>.</returns>
        public UniqueId CreateLong(long id)
        {
            return new UniqueId
            {
                Source = UniqueIdSource,
                IdType = IdTypeEnum.LongInt,
                Id = id.ToString(),
                SourceType = UniqueIdSourceType
            };
        }

        /// <summary>
        /// Creates an <see cref="UniqueId"/> of <see cref="IdTypeEnum"/>.String with the specified string value as the Id,
        /// and the factory's UniqueIdSource and UniqueIdSourceType values.
        /// </summary>
        /// <param name="id">The id value.</param>
        /// <returns>A new <see cref="UniqueId"/>.</returns>
        public UniqueId CreateString(string id)
        {
            return new UniqueId
            {
                Source = UniqueIdSource,
                IdType = IdTypeEnum.String,
                Id = id,
                SourceType = UniqueIdSourceType
            };
        }

        /// <summary>
        /// Creates an <see cref="UniqueId"/> of <see cref="IdTypeEnum"/>.URI with the specified string value as the Id,
        /// and the factory's UniqueIdSource and UniqueIdSourceType values.
        /// </summary>
        /// <param name="id">The id value.</param>
        /// <returns>A new <see cref="UniqueId"/>.</returns>
        public UniqueId CreateUri(string id)
        {
            return new UniqueId
            {
                Source = UniqueIdSource,
                IdType = IdTypeEnum.URI,
                Id = id,
                SourceType = UniqueIdSourceType
            };
        }

        /// <summary>
        /// Validates that the source specified is the same as the UniqueIdSource property.
        /// If not, throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="source">The source to check.</param>
        /// <exception cref="ArgumentException">Throw when the source does not match the UniqueIdSource property value.</exception>
        public void ValidateSource(string source)
        {
            if (!UniqueIdSource.Equals(source, StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException($"Unknown UniqueId source '{source}.'");
        }


        public bool ContainsId(CompoundIdentifier compoundId, string id, IdTypeEnum? type = null)
        {
            if (compoundId == null || string.IsNullOrWhiteSpace(id)) return false;

            var matches = compoundId.UniqueIds.Where(c =>
                c.Id?.Equals(id, StringComparison.InvariantCultureIgnoreCase) == true
                && c.Source?.Equals(UniqueIdSource, StringComparison.InvariantCultureIgnoreCase) == true);

            if (type.HasValue)
                matches = matches.Where(c => c.IdType.Equals(type));

            return matches.Any();
        }
    }
}
