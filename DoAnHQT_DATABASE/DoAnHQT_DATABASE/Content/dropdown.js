// Dữ liệu dropdown
const dropdownData = {
    'danh-muc': {
        sections: [
            {
                title: 'CHĂM SÓC DA MẶT',
                items: ['Sữa rửa mặt', 'Toner', 'Serum', 'Kem dưỡng ẩm', 'Mặt nạ', 'Kem chống nắng']
            },
            {
                title: 'TRANG ĐIỂM',
                items: ['Kem nền', 'Phấn phủ', 'Son môi', 'Mascara', 'Phấn mắt', 'Kẻ mắt']
            },
            {
                title: 'CHĂM SÓC CƠ THỂ',
                items: ['Sữa tắm', 'Kem dưỡng thể', 'Xà phòng', 'Dầu gội', 'Dầu xả', 'Sữa dưỡng thể']
            },
            {
                title: 'NƯỚC HOA',
                items: ['Nước hoa nữ', 'Nước hoa nam', 'Nước hoa unisex', 'Nước hoa mini']
            }
        ]
    },
    'thuong-hieu': {
        sections: [
            {
                title: 'THƯƠNG HIỆU CAO CẤP',
                items: ['La Roche-Posay', 'Vichy', 'Avene', 'Bioderma', 'Caudalie']
            },
            {
                title: 'THƯƠNG HIỆU HÀN QUỐC',
                items: ['Laneige', 'Innisfree', 'Etude House', 'Missha', 'The Face Shop']
            },
            {
                title: 'THƯƠNG HIỆU NHẬT BẢN',
                items: ['Shiseido', 'SK-II', 'Hada Labo', 'Kose', 'DHC']
            },
            {
                title: 'THƯƠNG HIỆU VIỆT NAM',
                items: ['Cocoon', 'Cá Heo', 'Simplee', 'Thorakao', 'Saigon Cosmetics']
            }
        ]
    },
    'cham-soc-da': {
        sections: [
            {
                title: 'THEO LOẠI DA',
                items: ['Da dầu', 'Da khô', 'Da hỗn hợp', 'Da nhạy cảm', 'Da mụn']
            },
            {
                title: 'THEO VẤN ĐỀ',
                items: ['Chống lão hóa', 'Giảm mụn', 'Dưỡng ẩm', 'Làm sáng da', 'Se khít lỗ chân lông']
            },
            {
                title: 'QUY TRÌNH CHĂM SÓC',
                items: ['Làm sạch', 'Cân bằng', 'Đặc trị', 'Dưỡng ẩm', 'Bảo vệ']
            },
            {
                title: 'SẢN PHẨM ĐẶC TRỊ',
                items: ['Tẩy tế bào chết', 'Mặt nạ', 'Tinh chất', 'Kem đặc trị', 'Xịt khoáng']
            }
        ]
    },
    'trang-diem': {
        sections: [
            {
                title: 'MẮT',
                items: ['Phấn mắt', 'Kẻ mắt', 'Mascara', 'Chì kẻ mày', 'Mi giả']
            },
            {
                title: 'MÔI',
                items: ['Son thỏi', 'Son lì', 'Son bóng', 'Son dưỡng', 'Chì kẻ viền môi']
            },
            {
                title: 'KHUÔN MẶT',
                items: ['Kem nền', 'Phấn phủ', 'Che khuyết điểm', 'Highlighter', 'Bronzer']
            },
            {
                title: 'DỤNG CỤ',
                items: ['Cọ trang điểm', 'Bông phấn', 'Gương', 'Kẹp mi', 'Bấm mi']
            }
        ]
    },
    'hang-moi': {
        sections: [
            {
                title: 'SẢN PHẨM MỚI',
                items: ['Bộ sưu tập mùa xuân', 'Sản phẩm limited edition', 'Công nghệ mới', 'Xu hướng mới nhất']
            },
            {
                title: 'KHUYẾN MÃI',
                items: ['Combo ưu đãi', 'Mua 1 tặng 1', 'Giảm giá sốc', 'Quà tặng đặc biệt']
            },
            {
                title: 'BÁN CHẠY',
                items: ['Top 10 sản phẩm', 'Được yêu thích nhất', 'Đánh giá cao', 'Hot trend']
            },
            {
                title: 'COMBO TIẾT KIỆM',
                items: ['Combo chăm sóc da', 'Combo trang điểm', 'Combo toàn diện', 'Combo du lịch']
            }
        ]
    }
};

// JavaScript để xử lý dropdown
document.addEventListener('DOMContentLoaded', function () {
    const dropdown = document.getElementById('fullscreenDropdown');
    const dropdownContent = document.getElementById('dropdownContent');
    const dropdownClose = document.getElementById('dropdownClose');
    const menuBars = document.querySelectorAll('.menu-bar');

    let hoverTimeout;
    let currentDropdownType = '';

    // Hàm hiển thị dropdown
    function showDropdown(dropdownType) {
        if (dropdownData[dropdownType]) {
            currentDropdownType = dropdownType;

            // Xóa nội dung cũ
            dropdownContent.innerHTML = '';

            // Thêm nội dung mới
            dropdownData[dropdownType].sections.forEach(section => {
                const sectionHTML = `
                                <div class="dropdown-section">
                                    <h4>${section.title}</h4>
                                    <ul>
                                        ${section.items.map(item => `<li><a href="#">${item}</a></li>`).join('')}
                                    </ul>
                                </div>
                            `;
                dropdownContent.innerHTML += sectionHTML;
            });

            // Hiển thị dropdown
            dropdown.classList.add('active');
        }
    }

    // Hàm ẩn dropdown
    function hideDropdown() {
        dropdown.classList.remove('active');
        currentDropdownType = '';
    }

    // Xử lý hover vào menu bar
    menuBars.forEach(menuBar => {
        menuBar.addEventListener('mouseenter', function () {
            const dropdownType = this.getAttribute('data-dropdown');

            // Clear timeout trước đó
            clearTimeout(hoverTimeout);

            // Hiển thị dropdown ngay lập tức
            showDropdown(dropdownType);
        });

        // Khi rời khỏi menu bar, không đóng dropdown ngay
        menuBar.addEventListener('mouseleave', function (e) {
            const relatedTarget = e.relatedTarget;

            // Chỉ đóng dropdown nếu chuột không di chuyển vào dropdown
            if (!dropdown.contains(relatedTarget)) {
                hoverTimeout = setTimeout(() => {
                    if (!dropdown.matches(':hover')) {
                        hideDropdown();
                    }
                }, 150); // Delay nhỏ để cho phép di chuyển chuột
            }
        });
    });

    // Xử lý hover vào dropdown
    dropdown.addEventListener('mouseenter', function () {
        clearTimeout(hoverTimeout);
    });

    dropdown.addEventListener('mouseleave', function () {
        hoverTimeout = setTimeout(() => {
            hideDropdown();
        }, 150);
    });

    // Đóng dropdown khi click nút close
    dropdownClose.addEventListener('click', function () {
        hideDropdown();
    });

    // Đóng dropdown khi click ra ngoài
    document.addEventListener('click', function (e) {
        if (!dropdown.contains(e.target) && !e.target.closest('.menu-bar')) {
            hideDropdown();
        }
    });

    // Xử lý di chuyển chuột giữa các menu bar và dropdown
    document.addEventListener('mousemove', function (e) {
        const isOverMenuBar = e.target.closest('.menu-bar');
        const isOverDropdown = e.target.closest('.fullscreen-dropdown');

        if (isOverMenuBar || isOverDropdown) {
            clearTimeout(hoverTimeout);
        }
    });
});