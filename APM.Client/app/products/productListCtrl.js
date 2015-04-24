(function () {
    "use strict";
    angular
        .module("productManagement")
        .controller("ProductListCtrl",
                    ["productResource",
                        ProductListCtrl]);

    function ProductListCtrl(productResource) {
        var vm = this;
        vm.searchCriteria = "GDN";

        productResource.query({search: vm.searchCriteria},function(data) {
            vm.products = data;
        });
        
        //productResource.query({$skip:1, $top:3},function(data) {
        //    vm.products = data;
        //});
        
        productResource.query({
            $filter: "contains(ProductCode,'GDN') and Price ge 5 and Price le 20",
            $orderby: "Price desc"
        }, function (data) {
            vm.products = data;
        });
        

    }
}());
