// SweetAlert2 Confirmation Functions
function confirmDelete(message, form) {
    Swal.fire({
        title: 'Konfirmasi Hapus',
        text: message || 'Apakah Anda yakin ingin menghapus ini? Tindakan ini tidak dapat dibatalkan.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#6c757d',
        confirmButtonText: 'Ya, Hapus!',
        cancelButtonText: 'Batal',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            if (form) {
                form.submit();
            }
        }
    });
    return false;
}

function confirmAction(title, message, confirmText, cancelText) {
    return Swal.fire({
        title: title || 'Konfirmasi',
        text: message || 'Apakah Anda yakin ingin melanjutkan?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#007bff',
        cancelButtonColor: '#6c757d',
        confirmButtonText: confirmText || 'Ya',
        cancelButtonText: cancelText || 'Batal',
        reverseButtons: true
    });
}

function confirmLogout() {
    Swal.fire({
        title: 'Logout',
        text: 'Apakah Anda yakin ingin keluar?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        confirmButtonText: 'Ya, Logout',
        cancelButtonText: 'Batal',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById('logoutForm').submit();
        }
    });
    return false;
}

// Success notification
function showSuccess(message) {
    Swal.fire({
        icon: 'success',
        title: 'Berhasil!',
        text: message,
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        position: 'top-end',
        toast: true
    });
}

// Error notification
function showError(message) {
    Swal.fire({
        icon: 'error',
        title: 'Gagal!',
        text: message,
        confirmButtonText: 'OK',
        confirmButtonColor: '#dc3545'
    });
}

// Info notification
function showInfo(message) {
    Swal.fire({
        icon: 'info',
        title: 'Informasi',
        text: message,
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        position: 'top-end',
        toast: true
    });
}

// Warning notification
function showWarning(message) {
    Swal.fire({
        icon: 'warning',
        title: 'Peringatan!',
        text: message,
        confirmButtonText: 'OK',
        confirmButtonColor: '#ffc107'
    });
}

// Loading overlay
function showLoading(message) {
    Swal.fire({
        title: message || 'Memproses...',
        allowOutsideClick: false,
        allowEscapeKey: false,
        showConfirmButton: false,
        didOpen: () => {
            Swal.showLoading();
        }
    });
}

function hideLoading() {
    Swal.close();
}
