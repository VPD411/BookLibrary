// Base URL API
const apiUrl = '/api/books';

// DOM Elements
const formTitle = document.getElementById('form-title');
const bookForm = document.getElementById('book-form');
const bookIdInput = document.getElementById('book-id'); // скрытый элемент, хранит id при редактировании
const formGroup = document.getElementById('form-group');
const titleInput = document.getElementById('title');
const authorInput = document.getElementById('author');
const genreInput = document.getElementById('genre');
const priceInput = document.getElementById('price');
const yearInput = document.getElementById('year');
const submitBtn = document.getElementById('submit-btn');
const cancelBtn = document.getElementById('cancel-btn');
const booksTable = document.getElementById('books-table');
const tbody = document.getElementById('books-tbody');

let isEditing = false; // edit - true (put), create - false (post)
let editingBookId = null; // ID редактируепмой книги

// При загрузке страницы получаем список книг
document.addEventListener('DOMContentLoaded', loadBooks);

// Обработчик отправки формы
bookForm.addEventListener('submit', handleFormSubmit);

// Кнопка отмены редактирования
cancelBtn.addEventListener('click', resetForm);

/// Загрузка всех книг с сервера
async function loadBooks() {
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) throw new Error('Ошибка загрузки данных');
        const books = await response.json();
        renderBooksTable(books);
    } catch (error) {
        console.error('Ошибка: ', error);
        alert('Не удалось загрузить список книг');
    }
}

// Отрисовка книг в таблице
function renderBooksTable(books) {
    tbody.innerHTML = '';

    books.forEach(book => {
        const row = document.createElement('tr');

        const idCell = document.createElement('td');
        idCell.textContent = book.id;

        const titleCell = document.createElement('td');
        titleCell.textContent = book.title;

        const authorCell = document.createElement('td');
        authorCell.textContent = book.author;

        const genreCell = document.createElement('td');
        genreCell.textContent = book.genre;

        const priceCell = document.createElement('td');
        priceCell.textContent = book.price;

        const yearCell = document.createElement('td');
        yearCell.textContent = book.year;

        const actionsCell = document.createElement('td');

        const editBtn = document.createElement('button');
        editBtn.className = 'action-btn edit-btn';
        editBtn.textContent = 'Редактировать';
        editBtn.addEventListener('click', () => editBook(book.id));

        const deleteBtn = document.createElement('button');
        deleteBtn.className = 'action-btn delete-btn';  
        deleteBtn.textContent = 'Удалить';             
        deleteBtn.addEventListener('click', () => deleteBook(book.id)); 

        actionsCell.appendChild(editBtn);
        actionsCell.appendChild(deleteBtn);

        row.appendChild(idCell);
        row.appendChild(titleCell);
        row.appendChild(authorCell);
        row.appendChild(genreCell);
        row.appendChild(priceCell);
        row.appendChild(yearCell);
        row.appendChild(actionsCell);

        tbody.appendChild(row);
    })
}


// Удаление книги
async function deleteBook(id) {
    try {
        const response = await fetch(`${apiUrl}/${id}`, { method: 'DELETE' });
        if (!response.ok) throw new Error('Ошибка удаления');
        loadBooks(); // Обновляем список
    } catch (error) {
        console.error('Ошибка:', error);
        alert('Не удалось удалить книгу.');
    }
}

// Заполнение формы для редактирования
async function editBook(id) {
    try {
        const response = await fetch(`${apiUrl}/${id}`);
        if (!response.ok) throw new Error('Ошибка обновления');
        const book = await response.json();

        // Сохраняем ID редактируемой книги
        editingBookId = book.id;
        bookIdInput.value = book.id;

        titleInput.value = book.title;
        authorInput.value = book.author;
        genreInput.value = book.genre;
        priceInput.value = book.price;
        yearInput.value = book.year;

        // Меняем форму для редактирования
        isEditing = true;
        formTitle.textContent = 'Редактировать книгу';
        submitBtn.textContent = 'Сохранить';
        cancelBtn.style.display = 'inline-block';
    } catch (error) {
        console.error('Ошибка:', error);
        alert('Не удалось загрузить данные книги');
    }
}

// Сброс формы в режим добавления
function resetForm() {
    bookForm.reset();
    bookIdInput.value = '';
    isEditing = false;
    editingBookId = null;
    formTitle.textContent = 'Добавить книгу';
    submitBtn.textContent = 'Добавить';
    cancelBtn.style.display = 'none';
}

// Обработка отправки формы (создание или обновление)
async function handleFormSubmit(event) {
    event.preventDefault();

    const book = {
        title: titleInput.value.trim(),
        author: authorInput.value.trim(),
        genre: genreInput.value.trim(),
        price: parseFloat(priceInput.value),
        year: parseInt(yearInput.value),
    };

    const url = isEditing ? `${apiUrl}/${editingBookId}` : apiUrl;
    const method = isEditing ? 'PUT' : 'POST';

    try {
        const response = await fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(book)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(errorText || 'Ошибка сохранения');
        }

        resetForm();
        loadBooks();

        alert(isEditing ? 'Книга обновлена' : 'Книга добавлена');
    } catch (error) {
        console.error('Ошибка:', error);
        alert('Не удалось сохранить книгу');
    }
}

