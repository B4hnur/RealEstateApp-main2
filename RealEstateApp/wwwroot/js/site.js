// RealEstateApp main JavaScript file

document.addEventListener('DOMContentLoaded', function () {
    // Initialize image upload preview
    initImageUpload();

    // Initialize filter form
    initFilterForm();

    // Initialize image gallery
    initImageGallery();
});

// Image upload preview functionality
function initImageUpload() {
    const imageUpload = document.getElementById('image-upload');
    const previewContainer = document.getElementById('image-preview-container');

    if (imageUpload && previewContainer) {
        imageUpload.addEventListener('change', function () {
            previewContainer.innerHTML = '';

            if (this.files) {
                const maxFiles = 10;
                const maxSize = 10 * 1024 * 1024; // 10MB
                let errorShown = false;

                // Check if too many files selected
                if (this.files.length > maxFiles) {
                    alert(`You can only upload up to ${maxFiles} images at once.`);
                    this.value = '';
                    return;
                }

                Array.from(this.files).forEach(file => {
                    // Check file size
                    if (file.size > maxSize) {
                        if (!errorShown) {
                            alert(`File ${file.name} is too large. Maximum size is 10MB.`);
                            errorShown = true;
                        }
                        return;
                    }

                    // Check if it's an image
                    if (!file.type.match('image.*')) {
                        if (!errorShown) {
                            alert(`File ${file.name} is not an image.`);
                            errorShown = true;
                        }
                        return;
                    }

                    // Create preview
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        const previewWrapper = document.createElement('div');
                        previewWrapper.className = 'image-preview-item';

                        const img = document.createElement('img');
                        img.src = e.target.result;
                        img.className = 'img-thumbnail';

                        const info = document.createElement('div');
                        info.className = 'image-info';
                        info.textContent = file.name;

                        previewWrapper.appendChild(img);
                        previewWrapper.appendChild(info);

                        previewContainer.appendChild(previewWrapper);
                    }

                    reader.readAsDataURL(file);
                });
            }
        });
    }
}

// Filter form functionality
function initFilterForm() {
    const filterForm = document.getElementById('filter-form');
    const resetButton = document.getElementById('reset-filter');

    if (filterForm && resetButton) {
        // Handle filter form reset
        resetButton.addEventListener('click', function (e) {
            e.preventDefault();

            // Clear all inputs
            const inputs = filterForm.querySelectorAll('input:not([type="submit"]), select');
            inputs.forEach(input => {
                if (input.type === 'checkbox') {
                    input.checked = false;
                } else {
                    input.value = '';
                }
            });

            // Submit the form
            filterForm.submit();
        });

        // Auto-submit on select change
        const selectElements = filterForm.querySelectorAll('select');
        selectElements.forEach(select => {
            select.addEventListener('change', function () {
                filterForm.submit();
            });
        });
    }
}

// Image gallery functionality
function initImageGallery() {
    const galleries = document.querySelectorAll('.property-gallery');

    galleries.forEach(gallery => {
        const mainImage = gallery.querySelector('.main-image img');
        const thumbnails = gallery.querySelectorAll('.thumbnail');

        if (mainImage && thumbnails.length > 0) {
            thumbnails.forEach(thumb => {
                thumb.addEventListener('click', function () {
                    // Update main image
                    mainImage.src = this.getAttribute('data-image');

                    // Update active state
                    thumbnails.forEach(t => t.classList.remove('active'));
                    this.classList.add('active');
                });
            });
        }
    });
}

// Delete image confirmation
function confirmDeleteImage(imageId, propertyId) {
    if (confirm('Are you sure you want to delete this image? This action cannot be undone.')) {
        const form = document.createElement('form');
        form.method = 'POST';
        form.action = `/Properties/DeleteImage`;
        form.style.display = 'none';

        const imageIdInput = document.createElement('input');
        imageIdInput.name = 'imageId';
        imageIdInput.value = imageId;

        const propertyIdInput = document.createElement('input');
        propertyIdInput.name = 'propertyId';
        propertyIdInput.value = propertyId;

        const antiForgeryToken = document.querySelector('input[name="__RequestVerificationToken"]').cloneNode(true);

        form.appendChild(imageIdInput);
        form.appendChild(propertyIdInput);
        form.appendChild(antiForgeryToken);

        document.body.appendChild(form);
        form.submit();
    }

    return false;
}

// Make image primary
function setAsPrimary(imageId, propertyId) {
    fetch(`/api/Images/SetPrimary/${imageId}?propertyId=${propertyId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Requested-With': 'XMLHttpRequest'
        },
        credentials: 'same-origin'
    })
        .then(response => {
            if (response.ok) {
                // Reload the page to see changes
                window.location.reload();
            } else {
                alert('There was an error setting the primary image.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('There was an error processing your request.');
        });

    return false;
}
