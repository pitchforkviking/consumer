var theCountApp = angular.module('theCountApp', []);

theCountApp.controller('theCountAppController', function ($scope, $http) {

    $scope.baseApiUrl = window.location.protocol + "//" + window.location.host + "/api/Consumer";

    $scope.isSignedIn = false;
    $scope.isSignIn = false;
    $scope.isSignUp = false;
    $scope.isAdd = false;

    $scope.emailID = '';

    $scope.eventList = [];


    $scope.message = 'Make a Memory';

    $scope.OpenSignIn = function () {
        $scope.isSignedIn = false;
        $scope.isSignIn = true;
        $scope.isSignUp = false;

        $scope.message = 'Sign In to Bring World Peace';
    }

    $scope.OpenSignUp = function () {
        $scope.isSignedIn = false;
        $scope.isSignIn = false;
        $scope.isSignUp = true;

        $scope.message = 'Sign Up for a Bright Future';
    }

    $scope.OpenHome = function () {
        $scope.isSignIn = false;
        $scope.isSignUp = false;
        $scope.isAdd = false;

        $scope.message = 'Home Sweet Home';
    }

    $scope.OpenAdd = function () {
        $scope.isAdd = true;
        $scope.message = 'Make a Memory';
    }

    $scope.SignIn = function (applicationUserObj) {
        
        $http.post($scope.baseApiUrl + '/SignIn', applicationUserObj)
            .then(
            function (data) {
                $scope.isSignedIn = true;
                $scope.emailID = applicationUserObj.EmailID;
                $scope.List();
                $scope.OpenHome();                
            },
            function (err) {
                $scope.message = 'I\'ve Failed You';
            })       
    }

    $scope.SignUp = function (applicationUserObj) {
        $http.post($scope.baseApiUrl + '/SignUp', applicationUserObj)
            .then(
            function (data) {
                $scope.isSignedIn = true;                
                $scope.message = 'Signed Up';
                $scope.emailID = applicationUserObj.EmailID;
                $scope.OpenHome();
            },
            function (err) {
                $scope.message = 'I\'ve Failed You';
            }) 
    }    

    $scope.Add = function (applicationEventObj) {
        applicationEventObj.EmailID = $scope.emailID;
        $http.post($scope.baseApiUrl + '/Add', applicationEventObj)
            .then(
            function (data) {
                $scope.message = 'Added';
                $scope.List();
                $scope.OpenHome();
            },
            function (err) {
                $scope.message = 'I\'ve Failed You';
            }) 
    }

    $scope.List = function () {
        $http.get($scope.baseApiUrl + '/List?partitionKey=' + $scope.emailID)
            .then(
            function (response) {
                $scope.eventList = response.data;
                //$scope.message = 'Added';
            },
            function (err) {
                $scope.message = 'I\'ve Failed You';
            })
    }

    $scope.Delete = function (rowKey) {
        $http.get($scope.baseApiUrl + '/Delete?partitionKey=' + $scope.emailID + '&rowKey=' + rowKey)
            .then(
            function (response) {
                $scope.List();
            },
            function (err) {
                $scope.message = 'I\'ve Failed You';
            })
    }

});