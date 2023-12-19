import React from 'react';
import ReactDOM from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";

import Root from "./routes/root";
import Index from './routes';
import ErrorPage from './error-page';

import Bikes, {
  loader as bikesLoader
} from './routes/bikes';

import Riders, {
  loader as ridersLoader
} from './routes/riders';

import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.css';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        errorElement: <ErrorPage />,
        children: [
          { index: true, element: <Index /> },
          {
            path: "bikes",
            element: <Bikes />,
            loader: bikesLoader,
          },
          {
            path: "riders",
            element: <Riders />,
            loader: ridersLoader
          },
        ],
      },
    ],
  },
]);

const root = ReactDOM.createRoot(document.getElementById('root'))
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();