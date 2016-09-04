IoTApp.createModule(
    'IoTApp.Dashboard.PieChart',
    (function () {
        var targetControl;
        'use strict';

        var refreshData = function refreshData(data) {
            //alert(JSON.stringify(data)); //data.devices.BYDDeviceStatus
            var bydStatus = {};
            bydStatus["3"] = 0;
            bydStatus["4"] = 0;
            bydStatus["5"] = 0;
            bydStatus["6"] = 0;
            bydStatus["7"] = 0;
            //alert(data.devices.length);
            for (var i = 0; i < data.devices.length; i++) {
                //alert(JSON.stringify(data.devices[i]));
                //alert(data.devices[i].bydDeviceStatus);
                bydStatus[data.devices[i].bydDeviceStatus] = bydStatus[data.devices[i].bydDeviceStatus] + 1;
            }
            //alert(JSON.stringify([bydStatus["3"],bydStatus["4"],bydStatus["5"],bydStatus["6"]]));
            var pieData = {
                labels: [
                    "故障",  //6         //1   
                    "關網",     //0         //0
                    "離網",        //0         //0
                    "待機",           //1         //0
                    "未知"                           //6
                ],
                datasets: [
                    {
                        data: [bydStatus["3"], bydStatus["4"], bydStatus["5"], bydStatus["6"], bydStatus["7"]],
                        backgroundColor: [
                            "#FF6384",
                            "#36A2EB",
                            "#FFCE56",
                            "#CCCE56",
                            "#000000",
                        ],
                        hoverBackgroundColor: [
                            "#FF6384",
                            "#36A2EB",
                            "#FFCE56",
                            "#DDCE65",
                            "#FFFFFF"
                        ]
                    }]
            };
            //Get the context of the canvas element we want to select
            var chart2 = $(targetControl);// ($(targetControl));
            new Chart(chart2, {
                type: 'pie',
                data: pieData
            });
            return;
        };
        var init = function (settings) {
            targetControl = settings.targetControl;
        };

        return {
            refreshData: refreshData,
            init: init
        }
    }), [jQuery, resources]);
