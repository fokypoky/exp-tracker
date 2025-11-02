'use client';

import { Page, PageHeader } from '@components';

export default function CategoriesPage() {
  return (
    <Page
      header={
        <PageHeader
          title="Категории"
          subtitle="Управляй категориями своих расходов и доходов"
          onAdd={() => {}}
        />
      }
    >
      <></>
    </Page>
  );
}
