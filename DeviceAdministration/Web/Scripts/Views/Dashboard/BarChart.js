IoTApp.createModule(
    'IoTApp.Dashboard.BarChart',
    (function () {
        var targetControl;
        var telemetryDataUrl;
        var labels; //["label1", "label2"]
        'use strict';

        var refreshData = function refreshData() {
            if (telemetryDataUrl) {
                $.ajax({
                    cache: true,
                    //complete: onRequestComplete,
                    url: telemetryDataUrl
                }).done(
                    function telemetryReadDone(data) {

                        var monthes = [];
                        var monthlyData = [];
                        for (var index = 0; index < data.data.data.length; index++) {
                            //alert(JSON.stringify(data.data.data[index]));
                            monthes.push(data.data.data[index].date);
                            monthlyData.push(data.data.data[index].errorCount);
                        }

                        var barChartData = {
                            labels: monthes,
                            datasets:
                                [
                                   {
                                       label: "故障",
                                       fillColor: "rgba(255, 100, 178, 0.2)",
                                       strokeColor: "rgba(255, 100, 178, 0.8)",
                                       highlightFill: "rgba(255, 100, 178, 0.75)",
                                       highlightStroke: "rgba(255, 100, 178, 1)",
                                       data: monthlyData
                                   }
                                ]

                        };

                        var chart2 = ($('#barChart'));
                        new Chart(chart2, {
                            type: 'bar',
                            data: barChartData
                        });
                        /*
                        new Chart(chart2).Bar(barChartData, {
                            responsive: true,
                            barValueSpacing: 5,
                            barDatasetSpacing: 1,
                            tooltipFillColor: "rgba(0,0,0,0.8)",
                            multiTooltipTemplate: "<%= datasetLabel %> -  <%= value %> "
                        });
                        */
                    }
                ).fail(function () {
                    alert('failed');
                    IoTApp.Helpers.Dialog.displayError(resources.unableToRetrieveDeviceTelemetryFromService);
                });
            }
        };
        var init = function (settings) {
            targetControl = settings.targetControl;
            telemetryDataUrl = settings.requestUrl;
        };

        return {
            refreshData: refreshData,
            init: init
        }
    }), [jQuery, resources]);
