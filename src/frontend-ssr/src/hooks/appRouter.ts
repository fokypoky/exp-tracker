'use client';

import { usePathname, useRouter } from 'next/navigation';
import { useCallback } from 'react';

export const useAppRouter = () => {
	const router = useRouter();
	const pathName = usePathname();

	const navigate = useCallback((url: string, absolute: boolean = false) => {
		router.push(absolute ? url : `${pathName}/${url}`);
	}, [router, pathName]);

	return navigate;
};
