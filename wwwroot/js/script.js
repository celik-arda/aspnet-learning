// Sayfa tamamen yüklendikten sonra çalışsın
document.addEventListener('DOMContentLoaded', async function () {

    // Canvas elementini seç
    const ctx = document.getElementById('myChart');


    // Controller'dan hesaplanan aylık verileri çek //
    const rawMonthlyDataList = await fetch("/Home/GetMonthlyGidersForChart");

    // Grafikte kullanabilmek için json'a çevir  //
    const monthlyGiderList = await rawMonthlyDataList.json();


    // Eğer canvas elementi varsa grafiği oluştur
    if (ctx) {
        const myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                // use created-fetched month names as chart labels//
                labels: monthlyGiderList.label,
                datasets: [{
                    label: 'Aylık Giderler (TL)',

                    // hesapladığım aylık giderler data-listesi //
                    // use calculated-fetched monthly giders as data //
                    data: monthlyGiderList.data,
                    backgroundColor: 'rgba(54, 162, 235, 0.5)',
                    borderColor: '#0c68f0',
                    borderWidth: 3
                }]
            },
            options: {
                responsive: true,

                // "maintainaspectratio" true olursa otomatik yükseklik, false olursa kendim ayarlıyorum //
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                },
                plugins: {
                    legend: {
                        display: true,
                        position: 'top'
                    }
                }
            }
        });
    }

});