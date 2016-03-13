
angular.module("anonymizer", ['ui.bootstrap','ui.format'])
    .controller("anonymizerController", function ($scope, $modal, $http) {

        $scope.anonymize = {};
        $scope.anonymize.template = null;
        $scope.currentField = null;
        $scope.fields = {};
        $scope.items = {};
        $scope.submitted = true; // Set this to false to initially hide validation errors

        $scope.init = function () {
            $scope.anonymize.fields = [];
            //$scope.anonymize.formats = [{ format: "$0", tokens: [{ field: null }] }];
            $scope.anonymize.formats = [];
            $scope.anonymize.items = [];
            $scope.anonymize.nameformat = null;
            $scope.anonymize.rename = 'None';
            if (!$scope.anonymize.replacements) {
                //$scope.anonymize.replacements = [{ "replace": "", "with": "" }];
                $scope.anonymize.replacements = [];
            }

            $scope.currentField = null;

            $scope.fields.filtered = [];
            $scope.fields.currentPage = 1;
            $scope.fields.itemsPerPage = 10;

            $scope.items.filtered = [];
            $scope.items.currentPage = 1;
            $scope.items.itemsPerPage = 100;
        };
        $scope.init();

        $scope.sliceFields = function () {
            var begin = (($scope.fields.currentPage - 1) * $scope.fields.itemsPerPage);
            var end = begin + $scope.fields.itemsPerPage;
            $scope.fields.filtered = $scope.anonymize.fields.slice(begin, end);
        };

        $scope.sliceItems = function () {
            var begin = (($scope.items.currentPage - 1) * $scope.items.itemsPerPage);
            var end = begin + $scope.items.itemsPerPage;
            $scope.items.filtered = $scope.anonymize.items.slice(begin, end);
        };

        $scope.anonymizeAllItems = function () {
            var anonymizeAllItems = $scope.anonymize.items.every(function (item) { return item.Anonymize; });
            return anonymizeAllItems;
        };

        $scope.anonymizeAllItemsClick = function () {
            var newValue = !$scope.anonymizeAllItems();
            angular.forEach($scope.anonymize.items, function (item) { item.Anonymize = newValue; });
        };

        $scope.$watch('anonymize.formats', function () {
            angular.forEach($scope.anonymize.formats, function (format) {
                format.tokennames = [];
                angular.forEach(format.tokens, function (token) {
                    if (token && token.field && token.field.Name) {
                        this.push(token.field.Name);
                    } else {
                        this.push('');
                    }
                }, format.tokennames);
            });
        }, true);

        $scope.getTemplates = function (filter) {
            return $http.get('/sitecore/admin/contentanonymizer/api/gettemplates', {
                params: {
                    filter: filter
                }
            }).then(function (response) {
                return response.data;
            });
        };
        
        $scope.onTemplateSelect = function ($item, $model, $label) {
            if ($item.Id) {
                $scope.init();

                $http.get('/sitecore/admin/contentanonymizer/api/getfields', {
                    params: {
                        templateId: $item.Id
                    }
                }).then(function (response) {
                    $scope.anonymize.fields = response.data;
                    $scope.sliceFields();
                });

                $http.get('/sitecore/admin/contentanonymizer/api/getitems', {
                    params: {
                        templateId: $item.Id
                    }
                }).then(function (response) {
                    $scope.anonymize.items = response.data;
                    $scope.sliceItems();
                });
            }
        };

        $scope.getFields = function(query) {
            return $scope.anonymize.fields;
        };

        $scope.setCurrentField = function (field) {
            $scope.currentField = field;
        }
        
        $scope.addFormat = function () {
            $scope.anonymize.formats.push({ format: "$0", tokens: [{ field: null }] });
        };

        $scope.removeFormat = function (index) {
            $scope.anonymize.formats.splice(index, 1);
        };

        $scope.selectFormat = function (format) {
            if ($scope.currentField === null) {
                $scope.anonymize.nameformat = format;
            } else {
                $scope.currentField.Format = format;
            }
        }

        $scope.addToken = function (format) {
            if (format) {
                format.tokens.push({ "field": null });
            } else {
                $scope.anonymize.nametokens.push({ "field": null });
            }
        };

        $scope.removeToken = function (index, format) {
            if (format) {
                format.tokens.splice(index, 1);
            } else {
                $scope.anonymize.nametokens.splice(index, 1);
            }
        };

        $scope.addReplacement = function () {
            $scope.anonymize.replacements.push({ "replace": "", "with": "" });
        };

        $scope.removeReplacement = function (index) {
            $scope.anonymize.replacements.splice(index, 1);
        };

        $scope.getMediaFolders = function (filter) {
            return $http.get('/sitecore/admin/contentanonymizer/api/getmediafolders', {
                params: {
                    filter: filter
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.submit = function(event) {
            $scope.submitted = true;
            if ($scope.form.$invalid) {
                event.preventDefault();
                return false;
            }

            var options = JSON.stringify($scope.anonymize);

            var modalInstance = $modal.open({
                templateUrl: 'processing.html'
            });

            $http.post('/sitecore/admin/contentanonymizer/api/anonymize', { options: options }).
                success(function (data, status, headers, config) {
                    modalInstance.close();
                    console.log('success');
                }).
                error(function (data, status, headers, config) {
                    modalInstance.close();
                    console.log('error');
                });

            return true;
        };
        
        

    });