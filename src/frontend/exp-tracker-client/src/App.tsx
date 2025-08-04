import { LogInPage } from '@pages/LogIn/LogInPage.tsx';
import { Route, Routes } from 'react-router-dom';

import './App.css';

const App = () => {
	return (
		<Routes>
			<Route path="/">
				<Route index element={ <LogInPage /> } />
			</Route>
		</Routes>
	);
};

export default App;
