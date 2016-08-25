IoTApp.createModule(
    'IoTApp.Dashboard.KPI',
    (function () {

        var targetControl;
    
        'use strict';
        var updateKPI = function (newData, fields) {
            var latest = newData[newData.length - 1];
         
            $(targetControl).text(latest.values._0101);
        };
    
        var init = function (settings) {
            targetControl = settings.targetControl;
        };
 

    return {
        updateKPI: updateKPI,
        init: init
    }
}), [jQuery, resources]);
/*
$(function () {
    "use strict";

    IoTApp.KPI.init();
});
*/