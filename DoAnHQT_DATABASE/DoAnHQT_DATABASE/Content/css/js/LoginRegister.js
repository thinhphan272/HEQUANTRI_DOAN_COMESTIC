// loginRegister.js
document.addEventListener('DOMContentLoaded', function () {
    // Lấy các phần tử DOM cần thiết
    const modal = document.querySelector('.modal');
    const modalOverlay = document.querySelector('.modal_overlay');
    const backBtns = document.querySelectorAll('.back-btn');
    const loginForm = document.querySelector('.login-form');
    const registerForm = document.querySelector('.register-form');
    const switchToRegisterBtn = document.querySelector('.switch-to-register');
    const switchToLoginBtn = document.querySelector('.switch-to-login');

    // Ẩn modal ban đầu
    modal.style.display = 'none';

    // Hàm hiển thị modal với form đăng nhập
    function showLoginModal() {
        modal.style.display = 'flex';
        loginForm.style.display = 'block';
        registerForm.style.display = 'none';
        document.body.style.overflow = 'hidden';
        modal.style.zIndex = '9999'; // Thêm dòng này
    }

    // Hàm hiển thị modal với form đăng ký
    function showRegisterModal() {
        modal.style.display = 'flex';
        loginForm.style.display = 'none';
        registerForm.style.display = 'block';
        document.body.style.overflow = 'hidden';
        modal.style.zIndex = '9999'; // Thêm dòng này
    }

    // Hàm ẩn modal
    function hideModal() {
        modal.style.display = 'none';
        document.body.style.overflow = 'auto';
    }

    // Hàm chuyển sang form đăng ký
    function switchToRegister() {
        loginForm.style.display = 'none';
        registerForm.style.display = 'block';
    }

    // Hàm chuyển sang form đăng nhập
    function switchToLogin() {
        registerForm.style.display = 'none';
        loginForm.style.display = 'block';
    }

    // Gắn sự kiện cho link "Đăng nhập / Đăng ký" trong menu
    const loginRegisterLink = document.querySelector('a[href*="About"]');
    if (loginRegisterLink) {
        loginRegisterLink.addEventListener('click', function (e) {
            e.preventDefault();
            showLoginModal();
        });
    }

    // Gắn sự kiện chuyển đổi form
    if (switchToRegisterBtn) {
        switchToRegisterBtn.addEventListener('click', switchToRegister);
    }

    if (switchToLoginBtn) {
        switchToLoginBtn.addEventListener('click', switchToLogin);
    }

    // Gắn sự kiện đóng modal cho tất cả nút back
    backBtns.forEach(btn => {
        btn.addEventListener('click', hideModal);
    });

    // Gắn sự kiện đóng modal khi click overlay
    if (modalOverlay) {
        modalOverlay.addEventListener('click', hideModal);
    }

    // Đóng modal khi nhấn phím ESC
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && modal.style.display === 'flex') {
            hideModal();
        }
    });

    // Ngăn sự kiện click trên modal body đóng modal
    const modalBody = document.querySelector('.modal_body');
    if (modalBody) {
        modalBody.addEventListener('click', function (e) {
            e.stopPropagation();
        });
    }
});