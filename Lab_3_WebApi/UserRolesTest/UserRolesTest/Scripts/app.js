//$(function () {
//    $('#logoffbtn').click(function () {
//        sessionStorage.removeItem('accessToken');
//        var cookies = document.cookie.split(";");

//        for (var i = 0; i < cookies.length; i++) {
//            var cookie = cookies[i];
//            var eqPos = cookie.indexOf("=");
//            var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
//            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
//        }
//        window.location.href = '/book/index'
//    })
//})

var app = angular.module('myApp', []);
app.run(function ($rootScope, $location) {
    $(".table").show();
    
});


app.controller('SageCtrl', ["$scope", "$http", "$filter", function ($scope, $http, $filter) {
    var accesstoken = sessionStorage.getItem('accessToken');

    var authHeaders = {};
    if (accesstoken) {
        authHeaders.Authorization = 'Bearer ' + accesstoken;
    }

    var sendRequest = function () {
        $http({
            url: 'http://localhost:38547/api/SagesApi/',
            method: "GET",
            headers: authHeaders
        }).then(function (response) {


            $scope.data = response.data;
        });
    }

    sendRequest();
    $scope.deleteUser = function (x) {
        console.log(x);
        $http({


            url: 'http://localhost:38547/api/SagesApi/' + x,
            method: "DELETE",
            headers: authHeaders
        }).then(function (response) {


            sendRequest();
        });;
    }
    $scope.sendData = function () {

        $http({

            data: {
                "Name": $scope.nameAuthor,
                "Age": $scope.ageAuthor
            },
            url: 'http://localhost:38547/api/SagesApi/',
            method: "POST",
            headers: authHeaders
        }).then(function (response) {


            sendRequest();
       
        });
    }

    $scope.updateUser = function (x) {        
        //var found = $filter('filter')($scope.data, { SageId: x }, true);
        //$scope.selected = found[0];
        //console.log($scope.selected);
        $http({

            data: {
                "Sage":
                    {
                    "Name": $scope.nameAuthor,
                    "Age": $scope.ageAuthor
                    }

                    
            },
            url: 'http://localhost:38547/api/SagesApi/'+x,
            method: "PUT",
            headers: authHeaders
        }).then(function (response) {


            sendRequest();

        });
    }
}]);



app.controller('BookCtrl', ["$scope", "$http", "$filter", function ($scope, $http, $filter) {
    var accesstoken = sessionStorage.getItem('accessToken');

    var authHeaders = {};
    if (accesstoken) {
        authHeaders.Authorization = 'Bearer ' + accesstoken;
    }

    $scope.loading = true;
              
    var sendRequest = function () {
        $http({
            url: 'http://localhost:38547/api/BooksApi/',
            method: "GET",
            headers: authHeaders
        }).then(function (response) {

            $scope.loading = false;
            $scope.data = response.data;
        });
    }

    sendRequest();

    $scope.deleteUser = function (x) {
        console.log(x);
        $http({


            url: 'http://localhost:38547/api/BooksApi/' + x,
            method: "DELETE",
            headers: authHeaders
        }).then(function (response) {


            sendRequest();
        });;
    }

    $scope.sendData = function () {

        
            $http({

                data: {
                    "Name": $scope.nameBook,
                    "Pages": $scope.pageCount
                    

                },
                url: 'http://localhost:38547/api/BooksApi/',
                method: "POST",
                headers: authHeaders
            }).then(function (response) {


                sendRequest();

            });
        

       
    }

    $scope.updateUser = function (x) {
        //var found = $filter('filter')($scope.data, { SageId: x }, true);
        //$scope.selected = found[0];
        //console.log($scope.selected);
        $http({

            data: {
                "Sage":
                    {
                    "Name": $scope.nameBook,
                    "Age": $scope.pageCount
                    }


            },
            url: 'http://localhost:38547/api/BooksApi/' + x,
            method: "PUT",
            headers: authHeaders
        }).then(function (response) {


            sendRequest();

        });
    }
}]);

app.service('loginservice', function ($http) {

    this.register = function (userInfo) {
        var resp = $http({
            url: "/api/Account/Register",
            method: "POST",
            data: userInfo,
        });
        return resp;
    };

    this.login = function (userlogin) {

        var resp = $http({
            url: "/TOKEN",
            method: "POST",
            data: $.param({ grant_type: 'password', username: userlogin.username, password: userlogin.password }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        });
        return resp;
    };
});

app.controller('logincontroller', function ($scope, loginservice) {

    //Scope Declaration
    $scope.responseData = "";

    $scope.userName = "";

    $scope.userRegistrationEmail = "";
    $scope.userRegistrationPassword = "";
    $scope.userRegistrationConfirmPassword = "";

    $scope.userLoginEmail = "";
    $scope.userLoginPassword = "";

    $scope.accessToken = "";
    $scope.refreshToken = "";
    //Ends Here

    //Function to register user
    $scope.registerUser = function () {

        $scope.responseData = "";

        //The User Registration Information
        var userRegistrationInfo = {
            Email: $scope.userRegistrationEmail,
            Password: $scope.userRegistrationPassword,
            ConfirmPassword: $scope.userRegistrationConfirmPassword
        };

        var promiseregister = loginservice.register(userRegistrationInfo);

        promiseregister.then(function (resp) {
            $scope.responseData = "User is Successfully";
            $scope.userRegistrationEmail = "";
            $scope.userRegistrationPassword = "";
            $scope.userRegistrationConfirmPassword = "";
        }, function (err) {
            $scope.responseData = "Error " + err.status;
        });
    };


    $scope.redirect = function () {
        window.location.href = '/Book/Api';
    };

    //Function to Login. This will generate Token 
    $scope.login = function () {
        //This is the information to pass for token based authentication
        var userLogin = {
            grant_type: 'password',
            username: $scope.userLoginEmail,
            password: $scope.userLoginPassword
        };

        var promiselogin = loginservice.login(userLogin);

        promiselogin.then(function (resp) {

            $scope.userName = resp.data.userName;
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            sessionStorage.setItem('userName', resp.data.userName);
            sessionStorage.setItem('accessToken', resp.data.access_token);
            sessionStorage.setItem('refreshToken', resp.data.refresh_token);
            window.location.href = '/Book/Api';
        }, function (err) {

            $scope.responseData = "Error " + err.status;
        });
    };
});
