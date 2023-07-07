import {Counter} from './components/Counter';
import {FetchData} from './components/FetchData';
import {Home} from './components/Home';
import Register from './components/Register';
import Personal from './components/Personal';

const AppRoutes = [
  {
    index: true,
    element: <Home />,
  },
  {
    path: '/counter',
    element: <Counter />,
  },
  {
    path: '/fetch-data',
    element: <FetchData />,
  },
  {
    path: '/register',
    element: <Register />,
  },
  {
    path: '/personal',
    element: <Personal />,
    restricted: true,
  },
];

export default AppRoutes;
