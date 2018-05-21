$("#logout-cancel").click(function (e) {

    e.preventDefault();
    window.location = $("input[name='ReturnUrl']").val();

})