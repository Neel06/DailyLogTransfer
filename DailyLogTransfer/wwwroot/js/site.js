var app = angular.module('myApp', ['ngSanitize']);

app.controller('myCntrl', ['$scope', '$sce', function($scope,$sce) {

    $scope.OutputResponse = '';

    $scope.GetTodaysLog = function () {

        $.ajax({
            method: 'POST',
            url: "/Home/GetTodaysLog",
            dataType: "HTML",
            success: onSuccessGetTodaysLog
        });
    };
    function onSuccessGetTodaysLog(response) {
        $scope.OutputResponse = $sce.trustAsHtml(response);
    }

    $scope.TransferFiles = function(FileName, FullPath) {
        console.log(FileName);
        console.log(FullPath);
        debugger;
        $.ajax({
            method: 'POST',
            data: { Filename: FileName, FullPath: FullPath },
            url: "/Home/TransferFiles",
            success: onSuccessTransferFiles,
        });
    };

     function onSuccessTransferFiles(response) {      
         console.log("File Transfered Successfully");
    }

}]);
