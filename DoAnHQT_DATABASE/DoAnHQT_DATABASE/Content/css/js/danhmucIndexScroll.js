document.addEventListener('DOMContentLoaded', function () {
    const scrollContainer = document.querySelector('.category-scroll');
    const scrollLeftBtn = document.querySelector('.scroll-left');
    const scrollRightBtn = document.querySelector('.scroll-right');
    const categoryItems = document.querySelector('.category-items');

    // Kích thước scroll mỗi lần
    const scrollAmount = 200;

    // Sự kiện click nút trái
    scrollLeftBtn.addEventListener('click', function () {
        scrollContainer.scrollBy({
            left: -scrollAmount,
            behavior: 'smooth'
        });
    });

    // Sự kiện click nút phải
    scrollRightBtn.addEventListener('click', function () {
        scrollContainer.scrollBy({
            left: scrollAmount,
            behavior: 'smooth'
        });
    });

    // Ẩn/hiện nút khi scroll
    scrollContainer.addEventListener('scroll', function () {
        // Ẩn nút trái nếu ở đầu
        if (scrollContainer.scrollLeft <= 0) {
            scrollLeftBtn.classList.add('hidden');
        } else {
            scrollLeftBtn.classList.remove('hidden');
        }

        // Ẩn nút phải nếu ở cuối
        if (scrollContainer.scrollLeft + scrollContainer.clientWidth >= scrollContainer.scrollWidth - 1) {
            scrollRightBtn.classList.add('hidden');
        } else {
            scrollRightBtn.classList.remove('hidden');
        }
    });

    // Kiểm tra ban đầu
    scrollContainer.dispatchEvent(new Event('scroll'));
});