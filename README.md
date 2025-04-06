# E-Trends - E-commerce Clothing Website

A modern e-commerce website built with Angular for selling clothing items. The application includes features like user authentication, product browsing, shopping cart, and responsive design.

## Features

- User Authentication (Login/Logout)
- Product Browsing with Categories
- Shopping Cart Management
- Responsive Design
- Modern UI/UX

## Prerequisites

- Node.js (v14 or higher)
- npm (v6 or higher)
- Angular CLI (v15 or higher)

## Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd Etrends
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
ng serve
```

4. Open your browser and navigate to `http://localhost:4200`

## Project Structure

```
src/
├── app/
│   ├── components/
│   │   ├── login/
│   │   ├── home/
│   │   ├── products/
│   │   ├── cart/
│   │   └── header/
│   ├── services/
│   │   ├── auth.service.ts
│   │   ├── product.service.ts
│   │   └── cart.service.ts
│   └── app.component.ts
├── assets/
└── styles.css
```

## Development

- The application uses Angular's standalone components
- Services are provided at the root level
- Routing is configured for all major features
- Responsive design is implemented using CSS Grid and Flexbox

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.
