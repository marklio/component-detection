using System.Collections.Generic;
using System.Linq;
using CycloneDX.Json;
using CycloneDX.Models;
using Microsoft.ComponentDetection.Contracts.BcdeModels;

namespace Microsoft.ComponentDetection.Contracts.Mappers
{
    public static class CycloneDx
    {
        public static string ToCycloneDx(this ScanResult scanResult)
        {
            return Serializer.Serialize(
                new Bom
                {
                    Components = scanResult.ComponentsFound.ToComponents(),
                });
        }

        private static Component ToComponent(this ScannedComponent scannedComponent)
        {
            return new Component
            {
                Type = Component.Classification.Library,
                Purl = scannedComponent.Component.PackageUrl.ToString(),
            };
        }

        private static List<Component> ToComponents(this IEnumerable<ScannedComponent> scannedComponents)
        {
            return scannedComponents.Select(sc => sc.ToComponent()).ToList();
        }
    }
}
