import { CategoryPage as Page } from '@components';

export default function CategoryPage ({ params }) {
	const { id } = params;
	
	return (
		<Page id={id} />
	);
}
