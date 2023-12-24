import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom"

import Root from "./routes/root"
import Index from './routes'
import ErrorPage from './error-page'

import Authentication, {
  action as authAction
} from './routes/auth'

import {
  action as logoutAction
} from './routes/logout'

import Bikes, {
  loader as bikesLoader,
} from './routes/bikes'

import Riders, {
  loader as ridersLoader,
} from './routes/riders'

import { tokenLoader } from './utils/auth'

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    loader: tokenLoader,
    errorElement: <ErrorPage />,
    children: [
      {
        errorElement: <ErrorPage />,
        children: [
          { index: true, element: <Index /> },
          {
            path: 'auth',
            element: <Authentication />,
            action: authAction,
          },
          {
            path: 'logout',
            action: logoutAction,
          },
          {
            path: "bikes",
            element: <Bikes />,
            loader: bikesLoader,
          },
          {
            path: "riders",
            element: <Riders />,
            loader: ridersLoader,
          },
        ],
      },
    ],
  },
])

export default function App() {
  return <RouterProvider router={router} />
}