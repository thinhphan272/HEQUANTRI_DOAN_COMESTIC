// Tăng giảm số lượng
$(document).ready(function () {
    // Xử lý nút GIẢM
    $('.js-decrease').on('click', function (e) {
        e.preventDefault(); // Ngăn chặn hành động mặc định của button

        // Tìm thẻ span chứa số lượng nằm cùng cấp (siblings) với nút bấm
        var $qtySpan = $(this).siblings('.js-quantity-number');
        var $qtyInput = $(this).siblings('.js-quantity-input');

        var currentQty = parseInt($qtySpan.text());

        // Chỉ giảm nếu số lượng > 1
        if (currentQty > 1) {
            currentQty--;
            $qtySpan.text(currentQty);
            $qtyInput.val(currentQty); // Cập nhật input ẩn
        }
    });

    // Xử lý nút TĂNG
    $('.js-increase').on('click', function (e) {
        e.preventDefault();

        var $qtySpan = $(this).siblings('.js-quantity-number');
        var $qtyInput = $(this).siblings('.js-quantity-input');

        var currentQty = parseInt($qtySpan.text());

        // Tăng số lượng (có thể thêm điều kiện if (currentQty < maxStock) ở đây)
        currentQty++;
        $qtySpan.text(currentQty);
        $qtyInput.val(currentQty); // Cập nhật input ẩn
    });
});


/// Submit register
$(document).ready(function () {
    // Bắt sự kiện khi form đăng ký được Submit
    $('#registerForm').submit(function (e) {
        e.preventDefault(); // 1. Ngăn chặn load lại trang mặc định

        var $errorBox = $('#register-error-msg');
        $errorBox.text(""); // Xóa lỗi cũ nếu có

        // 2. Gửi AJAX
        $.ajax({
            url: $(this).attr('action'), // Lấy URL từ thuộc tính action của form
            type: 'POST',
            data: $(this).serialize(), // Lấy toàn bộ dữ liệu trong form
            success: function (response) {
                if (response.success) {
                    // TRƯỜNG HỢP THÀNH CÔNG
                    alert(response.message);
                    location.reload(); // Load lại trang để đăng nhập hoặc cập nhật trạng thái
                } else {
                    // TRƯỜNG HỢP CÓ LỖI (Sai thông tin, trùng email...)
                    // Hiển thị thông báo lỗi ngay trong Modal
                    $errorBox.text(response.message);
                }
            },
            error: function () {
                $errorBox.text("Có lỗi xảy ra khi kết nối đến máy chủ.");
            }
        });
    });
});

//Submit đăng nhập
// ... Code đăng ký ở trên ...

// Bắt sự kiện khi form ĐĂNG NHẬP được Submit

$(document).ready(function () {
    // Dùng .on('submit') để bắt sự kiện chắc chắn hơn
    $(document).on('submit', '#loginForm', function (e) {
        e.preventDefault(); // Bước quan trọng nhất: Chặn load trang

        var $errorBox = $('#login-error-msg');
        var $btn = $(this).find('button[type="submit"]');

        $errorBox.text("");
        $btn.prop('disabled', true).text('Đang xử lý...'); // Khóa nút để tránh bấm nhiều lần

        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: $(this).serialize(),
            success: function (response) {
                if (response.success) {
                    //alert(response.message);
                    location.reload(); // Load lại trang khi thành công
                } else {
                    $errorBox.text(response.message);
                }
            },
            error: function () {
                $errorBox.text("Không thể kết nối đến máy chủ.");
            },
            complete: function () {
                // Mở khóa nút dù thành công hay thất bại
                $btn.prop('disabled', false).text('ĐĂNG NHẬP');
            }
        });
    });
});

$(document).ready(function () {

    // Hàm hiển thị Modal Đăng nhập/Đăng ký
    function showLoginModal() {
        // Giả sử modal của bạn có class là 'modal'
        // Điều chỉnh code này tùy theo cách bạn hiển thị/ẩn Modal
        $('.modal').fadeIn(300);
        $('.modal').css('display', 'flex');

        // Đảm bảo form đăng nhập được hiển thị (dựa trên cấu trúc bạn gửi)
        $('.auth-form.login-form').show();
        $('.auth-form.register-form').hide();
    }
});

    // Bắt sự kiện click vào nút "Thêm vào giỏ"
    $(document).ready(function () {
    // ... (Hàm showLoginModal giữ nguyên) ...

    // Bắt sự kiện click vào nút "Thêm vào giỏ"
    $(document).on('click', '.js-add-to-cart', function (e) {
        e.preventDefault();

        // Lấy ID sản phẩm từ thuộc tính data-product-id
        var pId = $(this).data('product-id');

        // Lấy số lượng từ input (class js-quantity-input bạn đã đặt ở phần tăng giảm)
        var qnt = $('.js-quantity-input').val();

        // Kiểm tra nếu không lấy được thì mặc định là 1
        if (!qnt) qnt = 1;

        $.ajax({
            url: '/Cart/ThemVaoGio', // Đường dẫn cứng hoặc dùng @Url.Action nếu script nằm trong file .cshtml
            type: 'POST', // Đổi thành POST cho bảo mật
            data: { productID: pId, sl: qnt }, // Tên tham số phải khớp với Controller (productID, sl)
            success: function (response) {
                if (response.requiresLogin === true) {
                    alert(response.message);
                    showLoginModal();
                } else if (response.success === true) {
                    //alert(response.message);
                    // Có thể reload trang hoặc cập nhật số trên icon giỏ hàng
                    location.reload(); 
                } else {
                    alert("Lỗi: " + response.message);
                }
            },
            error: function () {
                alert("Lỗi kết nối đến Server.");
            }
        });
    });
});