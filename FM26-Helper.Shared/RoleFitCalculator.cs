using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace FM26_Helper.Shared
{
    public static class RoleFitCalculator
    {
        private static List<RoleDefinition> _cachedRoles = new();

        static RoleFitCalculator()
        {
            LoadRoles();
        }

        private static void LoadRoles()
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "roles.json");
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    _cachedRoles = JsonSerializer.Deserialize<List<RoleDefinition>>(json) ?? new List<RoleDefinition>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading roles: {ex.Message}");
            }
        }

        public static List<RoleFitResult> Calculate(PlayerSnapshot player, string phase)
        {
            var results = new List<RoleFitResult>();
            var roles = _cachedRoles.Where(r => r.Phase.Equals(phase, StringComparison.OrdinalIgnoreCase));

            foreach (var role in roles)
            {
                double totalWeightedScore = 0;
                double maxPossible = 0;

                foreach (var weight in role.Weights)
                {
                    var attributeName = weight.Key;
                    var weightValue = weight.Value;

                    var attributeValue = GetAttributeValue(player, attributeName);

                    totalWeightedScore += attributeValue * weightValue;
                    maxPossible += 20 * weightValue;
                }

                double score = 0;
                if (maxPossible > 0)
                {
                    score = Math.Round((totalWeightedScore / maxPossible) * 100, 1);
                }

                results.Add(new RoleFitResult
                {
                    RoleName = role.Name,
                    Category = role.Category,
                    Score = score
                });
            }

            return results.OrderByDescending(r => r.Score).ToList();
        }

        private static int GetAttributeValue(PlayerSnapshot player, string attributeName)
        {
            if (player == null) return 0;

            // Check Technical
            if (player.Technical != null)
            {
                var prop = typeof(TechnicalAttributes).GetProperty(attributeName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop != null) return (int?)prop.GetValue(player.Technical) ?? 0;
            }

            // Check Mental
            if (player.Mental != null)
            {
                var prop = typeof(MentalAttributes).GetProperty(attributeName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop != null) return (int?)prop.GetValue(player.Mental) ?? 0;
            }

            // Check Physical
            if (player.Physical != null)
            {
                var prop = typeof(PhysicalAttributes).GetProperty(attributeName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop != null) return (int?)prop.GetValue(player.Physical) ?? 0;
            }

            // Check SetPieces
            if (player.SetPieces != null)
            {
                var prop = typeof(SetPieceAttributes).GetProperty(attributeName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop != null) return (int?)prop.GetValue(player.SetPieces) ?? 0;
            }

            return 0;
        }
    }
}
