namespace StudyCaseAGIT.Services
{
    public class ProductionBalancer
    {
        public static int[] BalanceProduction(int[] initialPlan)
        {
            int totalProduction = initialPlan.Sum();

            // Hitung jumlah hari kerja (hari dengan produksi > 0)
            int workingDays = initialPlan.Count(day => day > 0);

            // Jika tidak ada hari kerja, kembalikan array kosong
            if (workingDays == 0)
            {
                return new int[initialPlan.Length];
            }

            // Hitung rata-rata produksi berdasarkan hari kerja yang ada
            int averageProduction = totalProduction / workingDays;
            int remainder = totalProduction % workingDays;

            // Buat rencana produksi yang merata
            int[] balancedPlan = initialPlan
                .Select(day => day > 0 ? averageProduction : 0) // Hanya hari kerja yang mendapatkan rata-rata produksi
                .ToArray();

            // Distribusi sisa produksi ke hari kerja berdasarkan nilai produksi awal yang lebih tinggi
            var sortedIndices = initialPlan
                .Select((value, index) => new { value, index })
                .Where(x => x.value > 0) // Hanya menyertakan hari kerja
                .OrderByDescending(x => x.value)
                .Select(x => x.index)
                .ToList();

            for (int i = 0; i < remainder; i++)
            {
                balancedPlan[sortedIndices[i]] += 1;
            }

            return balancedPlan;
        }
    }
}
