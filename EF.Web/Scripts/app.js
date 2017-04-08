(function ($) {
    function Book() {
        var $thisthis = this;
        function initializeAddEditBook() {
            $(".datepicker").datepicker({
                "autoclose": true,
                "todayHighlight" : true
            });
            $(".datepicker").datepicker("setDate", new Date());
        }
        this.init = function () {
            initializeAddEditBook();
        };
    }
    $(function () {
        var self = new Book();
        self.init();
    });
}(jQuery));