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

namespace SampleObjects
{
    public class SampleRepository
    {
        private int _fieldBoundaryId = 0;
        private int _cropZoneId = 0;
        private SampleRepository()
        {
            Growers = new List<GrowerDto>();
            Farms = new List<FarmDto>();
            Fields = new List<FieldDto>();
            CropZones = new List<CropZoneDto>();
            FieldBoundaries = new List<FieldBoundaryDto>();
            CreateDtos();
        }

        private void CreateDtos()
        {
            for (int i = 0; i < DtoCount; i++)
            {
                var grower = new GrowerDto {Uid = Guid.NewGuid(), Name = $"Grower {i}"};
                AddFarms(grower);
                Growers.Add(grower);
            }
        }

        private void AddFarms(GrowerDto grower)
        {
            for (int i = 0; i < DtoCount; i++)
            {
                var farm = new FarmDto {Uid = Guid.NewGuid(), Name = $"Farm {1}", GrowerUid = grower.Uid};
                AddFields(farm);
                Farms.Add(farm);
            }
        }

        private void AddFields(FarmDto farm)
        {
            for (int i = 0; i < DtoCount; i++)
            {
                var field = new FieldDto {Uid = Guid.NewGuid(), Name = $"Field {i}", GrowerUid = farm.GrowerUid, FarmUid = farm.Uid};
                AddCropZones(field);
                AddFieldBoundaries(field);
                Fields.Add(field);
            }
        }

        private void AddCropZones(FieldDto field)
        {
            for (int i = 0; i < DtoCount; i++)
            {
                var id = _cropZoneId++;
                var cropZone = new CropZoneDto { Id = id, Name = $"CropZone {id}", FieldUid = field.Uid, CropYear = DateTime.Now.Year - i % DtoCount };
                CropZones.Add(cropZone);
            }
        }

        private void AddFieldBoundaries(FieldDto field)
        {
            for (int i = 0; i < DtoCount; i++)
            {
                var id = _fieldBoundaryId++;
                var boundaryDto = new FieldBoundaryDto() { Id = id, FieldUid = field.Uid, CropYear = DateTime.Now.Year - i % DtoCount };
                FieldBoundaries.Add(boundaryDto);
            }
        }

        public int DtoCount { get; } = 5;

        public static readonly SampleRepository Instance = new SampleRepository();

        public List<GrowerDto> Growers { get; set; }
        public List<FarmDto> Farms { get; set; }
        public List<FieldDto> Fields { get; set; }
        public List<CropZoneDto> CropZones { get; set; }
        public List<FieldBoundaryDto> FieldBoundaries { get; set; }
    }
}
