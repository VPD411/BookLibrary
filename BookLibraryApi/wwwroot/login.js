document.getElementById('login-form').addEventListener('submit', async (e) => {
    e.preventDefault();
    const username = document.getElementById('username').value.trim();
    const password = document.getElementById('password').value.trim();
    const errorDiv = document.getElementById('error-msg');
    errorDiv.classList.remove('show');

    try {
        const response = await fetch('/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ username, password })
        });

        if (response.ok) {
            const data = await response.json();
            localStorage.setItem('token', data.token);
            localStorage.setItem('username', data.username);
            localStorage.setItem('role', data.role);
            window.location.href = '/index.html';
        } else if (response.status === 401) {
            errorDiv.textContent = 'Неверные учетные данные';
            errorDiv.classList.add('show');
        } else {
            const err = await response.json();
            errorDiv.textContent = err.detail || 'Ошибка авторизации';
            errorDiv.classList.add('show');
        }
    } catch (error) {
        errorDiv.textContent = 'Сервер недоступен. Попробуйте позже';
        errorDiv.classList.add('show');
    }
});