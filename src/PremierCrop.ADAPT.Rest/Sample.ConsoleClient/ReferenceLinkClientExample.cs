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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AgGateway.ADAPT.ApplicationDataModel.Common;
using AgGateway.ADAPT.ApplicationDataModel.Documents;
using AgGateway.ADAPT.ApplicationDataModel.FieldBoundaries;
using AgGateway.ADAPT.ApplicationDataModel.Logistics;
using AgGateway.ADAPT.ApplicationDataModel.Prescriptions;
using AgGateway.ADAPT.ApplicationDataModel.Products;
using PremierCrop.ADAPT.Rest;

namespace Sample.ConsoleClient
{
    public class ReferenceLinkClientExample
    {
        private readonly ReferenceLinkClient _client;

        public ReferenceLinkClientExample(string apiBaseAddress)
        {
            _client = new ReferenceLinkClient(new HttpClient(), apiBaseAddress);
        }

        public async Task Run()
        {
            // Growers
            Console.WriteLine("Growers");
            var growers = await _client.Get<IReadOnlyCollection<ModelEnvelope<Grower>>>("/Growers");
            Console.WriteLine($"Growers count: {growers.Count}");

            var firstGrower = growers.First();
            Console.WriteLine($"First Grower Name: {firstGrower.Object.Name}.");

            var growerSelf = await _client.GetObjectByRel<Grower>(firstGrower.Links, Relationships.Self);
            Console.WriteLine($"Grower Self Name: {growerSelf.Object.Name}.");

            var farms = await _client.GetListByRel<Farm>(growerSelf.Links);
            Console.WriteLine($"Farms count for Grower Self: {farms.Count}");

            var fieldsByGrower = await _client.GetListByRel<Field>(growerSelf.Links);
            Console.WriteLine($"Fields count for Grower Self: {fieldsByGrower.Count}");

            // Farms
            Console.WriteLine();
            Console.WriteLine("Farms");
            var firstFarm = farms.First();
            Console.WriteLine($"First Farm Description: {firstFarm.Object.Description}.");
            var farmSelf = await _client.GetObjectByRel<Farm>(firstFarm.Links, Relationships.Self);
            Console.WriteLine($"Self Farm Description: {farmSelf.Object.Description}.");
            // Get owning grower
            var farmGrower = await _client.GetObjectByRel<Grower>(farmSelf.Links);
            Console.WriteLine($"Self Farm Grower Name: {farmGrower.Object.Name}.");
            // Get fields
            var fields = await _client.GetListByRel<Field>(farmSelf.Links);
            Console.WriteLine($"Self Farm Fields count: {fields.Count}.");

            Console.WriteLine();
            Console.WriteLine("Fields");
            var firstField = fields.First();
            Console.WriteLine($"First Field Description: {firstField.Object.Description}.");
            var fieldSelf = await _client.GetObjectByRel<Field>(firstField.Links, Relationships.Self);
            Console.WriteLine($"Self Field Description: {fieldSelf.Object.Description}.");
            // Get owning grower
            var fieldGrower = await _client.GetObjectByRel<Grower>(fieldSelf.Links);
            Console.WriteLine($"Self Field Grower Name: {fieldGrower.Object.Name}.");
            // Get owning farm
            var fieldFarm = await _client.GetObjectByRel<Farm>(fieldSelf.Links);
            Console.WriteLine($"Self Field Farm Description: {fieldFarm.Object.Description}.");
            // Get all Crop Zones
            var cropZones = await _client.GetListByRel<CropZone>(fieldSelf.Links);
            Console.WriteLine($"Self Field CropZones count: {cropZones.Count}.");
            // Get CropZones for current crop year by adding param.
            var cropYearCropZones = await _client.GetListByRel<CropZone>(fieldSelf.Links, DateTime.Now.Year.ToString());
            Console.WriteLine($"Self Field CropZones for Current Crop Year count: {cropYearCropZones.Count}.");
            // Get FieldBoundaries
            var boundaries = await _client.GetListByRel<FieldBoundary>(fieldSelf.Links);
            Console.WriteLine($"Self Field FieldBoundaries count: {boundaries.Count}.");
            // Get FieldBoundaries for current crop year by adding param.
            var cropYearBoundaries = await _client.GetListByRel<FieldBoundary>(fieldSelf.Links, DateTime.Now.Year.ToString());
            Console.WriteLine($"Self Field FieldBoundaries for Current Crop Year count: {cropYearBoundaries.Count}.");

            Console.WriteLine();
            Console.WriteLine("CropZones");
            var firstCropZone = cropYearCropZones.First();
            Console.WriteLine($"First CropZone Description: {firstCropZone.Object.Description}.");
            var cropZoneSelf = await _client.GetObjectByRel<CropZone>(firstCropZone.Links, Relationships.Self);
            Console.WriteLine($"Self CropZone Description: {cropZoneSelf.Object.Description}.");
            // Get owning field
            var cropZoneField = await _client.GetObjectByRel<Field>(cropZoneSelf.Links);
            Console.WriteLine($"Self CropZone Field Description: {cropZoneField.Object.Description}.");


            Console.WriteLine();
            Console.WriteLine("FieldBoundaries");
            var firstBoundary = cropYearBoundaries.First();
            Console.WriteLine($"First FieldBoundary Description: {firstBoundary.Object.Description}.");
            var boundarySelf = await _client.GetObjectByRel<FieldBoundary>(firstBoundary.Links, Relationships.Self);
            Console.WriteLine($"Self FieldBoundary Description: {boundarySelf.Object.Description}.");
            // Get owning field
            var boundaryField = await _client.GetObjectByRel<Field>(boundarySelf.Links);
            Console.WriteLine($"Self FieldBoundary Field Description: {boundaryField.Object.Description}.");

        }
    }
}
