document.getElementById('contactForm').addEventListener('submit', function (event) {
    event.preventDefault(); // Prevent default form submission

    // Collect form data
    const formData = {
        FullName: document.getElementById('fullName').value,
        Email: document.getElementById('email').value,
        PhoneNumber: document.getElementById('phoneNumber').value,
        Subject: document.getElementById('subject').value,
        Message: document.getElementById('message').value
    };

    // Send data to the backend API
    fetch('http://localhost:5026/api/contact', {  // ✅ Update API URL with correct port
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Failed to send message');
        }
        return response.json();
    })
    .then(data => {
        alert('Message sent successfully! ✅');
        document.getElementById('contactForm').reset();
    })
    .catch((error) => {
        console.error('Error:', error);
        alert('Failed to send message ❌. Please try again.');
    });
});