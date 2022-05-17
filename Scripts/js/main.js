'use strict';
$(document).ready(function() {

    // Accordion
    var all_panels = $('.BEMI-accordion > li > ul').hide();

    $('.BEMI-accordion > li > a').click(function() {
        console.log('Hello world!');
        var target = $(this).next();
        if (!target.hasClass('active')) {
            all_panels.removeClass('active').slideUp();
            target.addClass('active').slideDown();
        }
        return false;
    });
    // End accordion

    // Product detail
    $('.product-links-wap a').click(function() {
        var this_src = $(this).children('img').attr('src');
        $('#product-detail').attr('src', this_src);
        return false;
    });
    $('#btn-minus').click(function() {
        var val = $("#var-value").html();
        val = (val == '1') ? val : val - 1;
        $("#var-value").html(val);
        $("#product-quanity").val(val);
        return false;
    });
    $('#btn-plus').click(function() {
        var val = $("#var-value").html();
        val++;
        $("#var-value").html(val);
        $("#product-quanity").val(val);
        return false;
    });
    $('.btn-size').click(function() {
        var this_val = $(this).html();
        $("#product-size").val(this_val);
        $(".btn-size").removeClass('btn-secondary');
        $(".btn-size").addClass('btn-success');
        $(this).removeClass('btn-success');
        $(this).addClass('btn-secondary');
        return false;
    });

    $('.btn-color').click(function () {
        var this_val = $(this).html();
        $("#product-color").val(this_val);
        $(".btn-color").removeClass('btn-secondary');
        $(".btn-color").addClass('btn-success');
        $(this).removeClass('btn-success');
        $(this).addClass('btn-secondary');
        return false;
    });

    $('.deleteProduct').click(function () {
        var this_val = $(this).data('id');
        $(".modal-body #productID").val(this_val);
        return false;
    });

    //$('.btn-size').click(function () {
    //    var this_val = $(this).html();
    //    $("#product-color").val(this_val);
    //    return false;
    //});
    // End roduct detail

});

/* -----cart page-------- */
$('.like-btn').on('click', function() {
    $(this).toggleClass('is-active');
});
 