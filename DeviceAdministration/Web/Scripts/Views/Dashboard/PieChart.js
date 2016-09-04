IoTApp.createModule(
    'IoTApp.Dashboard.PieChart',
    (function () {
        var targetControl;
        var labels; //["label1", "label2"]
        'use strict';
        var updateChart = function (newData, fields) {
            var latest = newData[newData.length - 1];

            $(targetControl).text(latest.values._0101);

            var myPieChart = new Chart($(targetControl), {
                type: 'pie',
                data: data,
                options: options
            });
        };

        var init = function (settings) {
            targetControl = settings.targetControl;
        };

        return {
            updateChart: updateChart,
            init: init
        }
    }), [jQuery, resources]);
